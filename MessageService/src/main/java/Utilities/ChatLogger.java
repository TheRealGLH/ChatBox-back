package Utilities;

import java.io.File;
import java.nio.file.InvalidPathException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.logging.*;

public class ChatLogger {
    private static Logger LOGGER = Logger.getLogger("ChatLogger");
    private static SimpleDateFormat dateFormat = new SimpleDateFormat("YYYY-MMM-dd k-m-s-S");
    private static ChatLogger instance = new ChatLogger();
    private boolean writeToFile = Boolean.parseBoolean(PropertiesLoader.getPropValues("logger.writeToFile", "logger.properties"));
    private Handler fileHandler;
    private Formatter formatter;


    private ChatLogger() {
        Level loggingLevel = Level.parse(PropertiesLoader.getPropValues("logger.logLevel", "logger.properties"));
        LOGGER.setLevel(loggingLevel);
        try {
            if (writeToFile) {
                //The path we'll create will have subdirectories for every executable
                String name = System.getProperty("sun.java.command").replace('/', '.');
                Date today = new Date();
                String logPath = PropertiesLoader.getPropValues("logger.logPath", "logger.properties") + name;

                //Check if the path exists. if not, we make it
                File dirCheck = new File(logPath);
                if (dirCheck.mkdirs()) {
                    System.out.println("Creating log directory at " + dirCheck.getAbsolutePath());
                }
                try {
                    fileHandler = new FileHandler(logPath + "\\" + dateFormat.format(today) + ".log");
                    LOGGER.addHandler(fileHandler);
                    formatter = new SimpleFormatter();
                    fileHandler.setFormatter(formatter);
                    fileHandler.setLevel(loggingLevel);
                } catch (Exception e) {
                    LOGGER.log(Level.SEVERE, "Error initializing the logger.", e);
                }
            }


        } catch (
                InvalidPathException  e) {
            LOGGER.log(Level.SEVERE, "The specified file path was invalid. Not writing to file for this " +
                    "session: " + e.getMessage());
        }

    }


    /**
     * Logs a message to the logger
     *
     * @param level   The level to log the message at
     * @param message The message to log
     */
    public static void Log(Level level, String message) {
        LOGGER.log(level, message);
    }

    /**
     * Logs a message with an object to the logger
     *
     * @param level   The level to log the message at
     * @param message The message to log
     * @param object  The object related to the message
     */
    public static void Log(Level level, String message, Object object) {
        LOGGER.log(level, message, object);
    }


}
