FROM nginx:latest

RUN rm /etc/nginx/conf.d/default.conf

copy ./nginx.conf /etc/nginx/nginx.conf

copy . /etc/nginx/html

#COPY ["/static", "/usr/share/nginx/html"]