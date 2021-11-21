This project is the complex solution for Labs 4-6

It is a Webapi that uses:

1. Azure Table Storage - for reading, modifying, saving data
2. Azure Queue Storage - on POST, it sends a message to a queue and an Azure Function App should subscribe to that queue and perform what is needed
3. Dockerfile that builds an image. This image should be pushed to Docker Hub and from then it can be used for deploy in Azure


# Docker instructions:
1. Build: docker build -t radupetrusan/test:students-api .   
2. Run (optionally): docker run -ti --rm -p 8080:80 radupetrusan/test:students-api
3. If you run this, it should be accesible at http://localhost:8080/students

