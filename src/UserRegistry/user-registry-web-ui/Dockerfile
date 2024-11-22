# Build Angular application.
FROM node:20.17.0 AS builder
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build --prod

# Serve the application.
FROM nginx:alpine
EXPOSE 80
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=builder /app/dist/user-registry-web-ui/browser /usr/share/nginx/html
CMD ["nginx", "-g", "daemon off;"]