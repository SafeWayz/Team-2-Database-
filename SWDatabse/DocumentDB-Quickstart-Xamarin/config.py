import os

settings = {
    'host': os.environ.get('ACCOUNT_HOST', 'DOCUMENTDBURL'),
    'master_key': os.environ.get('ACCOUNT_KEY', 'DOCUMENTDBSECRET'),
    'database_id': os.environ.get('COSMOS_DATABASE', 'DOCUMENTDBDATABASEID'),
    'container_id': os.environ.get('COSMOS_CONTAINER', 'DOCUMENTDBCOLLECTIONID'),
}