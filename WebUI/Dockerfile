# Etapa base com Node.js e Angular CLI
FROM node:20 AS build
WORKDIR /app

# Copia arquivos de dependência e instala dependências
COPY WebUI/package*.json ./
RUN npm install -g @angular/cli
RUN npm install

# Copia todo o projeto Angular
COPY WebUI/ .

# Build de produção (ou use `npm start` se preferir execução dev)
RUN ng build --configuration=production

# Etapa final: servidor Nginx para servir o app
FROM nginx:alpine
COPY --from=build /app/dist/*/browser /usr/share/nginx/html

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
