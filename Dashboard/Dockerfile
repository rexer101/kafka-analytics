FROM node:20.12.2 as build-step
    RUN mkdir -p /app
    WORKDIR /app
    COPY DashBoard/package.json /app
    RUN npm install
    COPY /DashBoard /app
    RUN npm run build --prod

FROM nginx:1.20.1
    COPY --from=build-step /app/dist/dash-board /usr/share/nginx/html
    EXPOSE 4200:80