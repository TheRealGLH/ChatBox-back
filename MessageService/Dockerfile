# ============= #
# === Build === #
# ============= #
FROM openjdk:16 AS build

WORKDIR /MessageService

# Copy all of the source files into our image
COPY . ./

# Create a clean build of the app
RUN ./mvnw clean compile assembly:single


# ================== #
# === Production === #
# ================== #
FROM adoptopenjdk:16-jre AS production

# Retrieve the fresh build and rename it to app.jar
COPY --from=build "/MessageService/target/MessageService-1.0-SNAPSHOT-jar-with-dependencies.jar" app.jar

EXPOSE 8095

# Run the application
ENTRYPOINT ["java", "-jar", "app.jar"]