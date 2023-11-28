FROM ubuntu:latest
RUN apt update
RUN apt install -y ghostscript
RUN apt install -y tesseract-ocr
RUN apt install -y rabbitmq-server
RUN apt install -y python3
RUN apt install -y pip
RUN pip install pika
RUN pip install minio
RUN pip install psycopg2-binary

ENTRYPOINT ["python3", "app/consume.py"]