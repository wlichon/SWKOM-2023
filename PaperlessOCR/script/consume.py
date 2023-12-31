import pika
from minio import Minio
import time
import subprocess
import os
import psycopg2
from elasticsearch import Elasticsearch     #install this : "pip install elasticsearch"
from datetime import datetime

DOCUMENT_PATH = "/app/documents/"
OUTPUT_PATH = "/app/gs-out/"

def save_to_elasticsearch(document_id, title, content):
    es = Elasticsearch('http://elasticsearch:9200')

    doc = {
        'document_id': document_id,
        'title': title,
        'content': content,
    }
    resp = es.index(index="swkom2023-documents", id=document_id, document=doc)
    print(resp['result'])


def callback(ch, method, properties, body):
    document_id = properties.message_id
    print(f"DOCUMENT ID: {document_id}")
    print(f"Received message: {body}")
    fileName = body.decode('utf-8')
    # Connect to Minio server
    minio_client = Minio('minio:9000',
                            access_key='adkBQ7HSGAS34lJozZyj',
                            secret_key='DU9xjAdodw409joJUnw6A5tbjBsEeU8mbIfXgDWK',
                            secure=False)

    # Specify your Minio bucket and PDF file name
    bucket_name = 'paperless'
    

    # Download the PDF file from Minio
    try:
        full_path = os.path.join(DOCUMENT_PATH, fileName)
        minio_client.fget_object(bucket_name, fileName, full_path)
        print(f"Downloaded PDF file: {fileName}")
        file_suffix = os.path.splitext(fileName)[1]
        print(file_suffix)
        if file_suffix == ".pdf":
            try:
                convert_pdf_to_image(full_path)
                execute_tesseract()
                try:
                    file_path = 'app/ocr.txt'
                    with open(file_path, 'r') as file:
                        file_content = file.read()
                    save_to_postgres(document_id, file_content)
                    save_to_elasticsearch(document_id, fileName, file_content)
                except OSError:
                    print("failed to store content in db")

            except OSError:
                print("Failed to execute gs or tesseract")

    except Exception as e:
        print(f"Error downloading PDF file: {e}")

def establish_connection():
    try:
        connection = pika.BlockingConnection(pika.ConnectionParameters('rabbitmq', credentials=pika.PlainCredentials('admin', 'password')))
        return connection
    except pika.exceptions.AMQPConnectionError:
        print("Failed to connect to RabbitMQ. Retrying in 5 seconds...")
        time.sleep(5)
        return establish_connection()

def execute_tesseract():
    tesseract_command = f"tesseract app/gs-out/output.png app/ocr"
    subprocess.run(tesseract_command, shell=True)
    
def convert_pdf_to_image(full_path):
    ghostscript_command = f"gs -dNOPAUSE -sDEVICE=png16m -r300 -sOutputFile=/app/gs-out/output.png '{full_path}' -c quit"
    p = subprocess.Popen(ghostscript_command, shell=True)
    p_status = p.wait()


def save_to_postgres(document_id, content):
    connection = psycopg2.connect(
        host="local_pgdb",
        database="admin",
        user="admin",
        password="password",
        port=5432
    )
    cursor = connection.cursor()
    cursor.execute('UPDATE "Documents" SET content = %s WHERE id = %s', (content, document_id))
    connection.commit()
    cursor.close()
    connection.close()

connection = establish_connection()
channel = connection.channel()

channel.queue_declare(queue='paperless')

channel.basic_consume(queue='paperless', on_message_callback=callback, auto_ack=True)

print('Waiting for messages. To exit press CTRL+C')
channel.start_consuming()
