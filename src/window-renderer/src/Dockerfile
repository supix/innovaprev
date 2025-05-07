FROM node:18-slim as builder

WORKDIR /app

COPY src/window-renderer/src/package.json ./

RUN npm i

COPY src/window-renderer/src .

RUN npm run build

FROM node:18-slim

WORKDIR /app

COPY --from=builder /app/dist ./dist
COPY --from=builder /app/node_modules ./node_modules
COPY --from=builder /app/package.json ./

CMD ["node", "dist/index.js"]
