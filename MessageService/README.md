# MessageService

A Java application using Maven as its package manager.
Runs on port 8095 by default.

## Building binaries

Navigate to the ``MessageService/`` directory in your CLI and run:
```
mvn clean compile assembly:single
```
This format is needed to include dependencies in the package.
The .jar file will then be placed in the ``MessageService/target/`` directory.

## Docker Container

In the ``MessageService/`` directory, run this:

```
docker build --tag messageservice .
```
