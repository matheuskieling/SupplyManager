docker compose -f ./compose.yaml down
docker volume rm supplymanager_postgres_data
docker compose up -d database