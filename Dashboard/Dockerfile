FROM node:20.12.2 as build-step
    RUN mkdir -p /app
    WORKDIR /app
    COPY Dashboard/package.json /app
    RUN npm install
    COPY /Dashboard /app
    RUN npm run build --prod

FROM nginx:1.20.1
    COPY --from=build-step /app/dist/dashboard /usr/share/nginx/html
    EXPOSE 4200:80