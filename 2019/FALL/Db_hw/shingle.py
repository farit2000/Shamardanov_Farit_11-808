import psycopg2

conn = psycopg2.connect(
    dbname='faritdb',
    user='farit@faritdb',
    password='79655846050Ubuntu',
    host='faritdb.postgres.database.azure.com',
    port='5432',
    sslmode='require'
)

print("Database opened succsessfully")
print(1+1)
