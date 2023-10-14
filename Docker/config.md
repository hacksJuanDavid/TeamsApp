## Configuring containers database using Docker Compose

### Create a docker-compose.yml file

```
version: "3.8"
services:
  mysql1:
    image: mysql:latest
    restart: always
    container_name: mysql1_container_teamservice
    environment:
      MYSQL_ROOT_PASSWORD: root
      MYSQL_DATABASE: teamsservicedb
      MYSQL_USER: serviceteamsuser
      MYSQL_PASSWORD: T3M34s4msUs3r*01
    ports:
      - "3307:3307"
    volumes:
      - dbdata1:/var/lib/mysql

  mysql2:
    image: mysql:latest
    restart: always
    container_name: mysql2_container_memberservice
    environment:
      MYSQL_ROOT_PASSWORD: root
      MYSQL_DATABASE: membersservicedb
      MYSQL_USER: servicememberuser
      MYSQL_PASSWORD: M34msS3rv1c3Us3r*01
    ports:
      - "3308:3308"
    volumes:
      - dbdata2:/var/lib/mysql

volumes:
  dbdata1:
  dbdata2:
```

## Start the containers

```
docker compose up -d
```

## Stop the containers

```
docker compose down
```

## Check the status of the containers

```
docker compose ps

CONTAINER ID   IMAGE          COMMAND                  CREATED          STATUS          PORTS                                                  NAMES
93232b269362   mysql:latest   "docker-entrypoint.s…"   35 minutes ago   Up 35 minutes   33060/tcp, 0.0.0.0:3308->3306/tcp, :::3308->3306/tcp   mysql2_container_memberservice
5d0ec32fc851   mysql:latest   "docker-entrypoint.s…"   35 minutes ago   Up 35 minutes   33060/tcp, 0.0.0.0:3307->3306/tcp, :::3307->3306/tcp   mysql1_container_teamservice
```

## Check ports run in the host

```
sudo netstat -tuln

tcp        0      0 0.0.0.0:3307            0.0.0.0:*               LISTEN
tcp        0      0 0.0.0.0:3308            0.0.0.0:*               LISTEN

// This ip address run in the host
0 0.0.0.0 = localhost
```

## Ip address of the contianer mysql1_container database membersservicedb Host:3308

```
docker inspect mysql2_container_memberservice
docker inspect mysql2_container_memberservice | grep IPAddress
```

## Ip address of the contianer mysql2_container database teamservicedb Host:3307

```
docker inspect mysql1_container_teamservice
docker inspect mysql1_container_teamservice | grep IPAddress
```

## Configure the database connection in the microservice membersservice modificate the appsettings.json file

```
"CnnStr": "server=localhost;port=3308;database=membersservicedb;user=servicememberuser;password=M34msS3rv1c3Us3r*01"
```

## Configure the database connection in the microservice teamservice modificate the appsettings.json file

```
"CnnStr": "server=localhost;port=3307;database=teamsservicedb;user=serviceteamsuser;password=T3M34s4msUs3r*01"
```

## Acces to the container mysql1_container database membersservicedb

```
docker exec -it mysql2_container_memberservice bash
```

## Acces to the container mysql2_container database teamsservicedb

```
docker exec -it mysql1_container_teamservice bash
```

## Verificate in bash run the mysql

```
mysql --version
mysql  Ver 8.0.26 for Linux on x86_64 (MySQL Community Server - GPL)
```

## Execute in bash terminal mysql1_container database membersservicedb

```
mysql -u root -p
mysql -u servicememberuser -p
```

## Execute in bash terminal mysql2_container database teamsservicedb

```
mysql -u root -p
mysql -u serviceteamsuser -p

// Check the databases
mysql> show databases;
+--------------------+
| Database           |
+--------------------+
| information_schema |
| membersservicedb   |
| performance_schema |
+--------------------+
3 rows in set (0.00 sec)

```
