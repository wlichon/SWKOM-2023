import pika
from minio import Minio
import time
import subprocess
import os

DOCUMENT_PATH = "/app/documents/"
OUTPUT_PATH = "/app/gs-out/"

def callback(ch, method, properties, body):
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
            convert_pdf_to_image(full_path)

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
    
def convert_pdf_to_image(full_path):
    # Use Ghostscript to convert PDF to image
    ghostscript_command = f"gs -dNOPAUSE -sDEVICE=png16m -r300 -sOutputFile=/app/gs-out/output.png {full_path} -c quit"
    subprocess.run(ghostscript_command, shell=True)
    

connection = establish_connection()
channel = connection.channel()

channel.queue_declare(queue='paperless')

channel.basic_consume(queue='paperless', on_message_callback=callback, auto_ack=True)

print('Waiting for messages. To exit press CTRL+C')
channel.start_consuming()
