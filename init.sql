\set dbname 'Tests'

SELECT 'CREATE DATABASE "' || :'dbname' || '"'
WHERE NOT EXISTS (
    SELECT FROM pg_database WHERE datname = :'dbname'
)\gexec
