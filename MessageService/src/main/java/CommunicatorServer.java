

import Utilities.ChatLogger;
import Utilities.PropertiesLoader;
import org.eclipse.jetty.server.Server;
import org.eclipse.jetty.server.ServerConnector;
import org.eclipse.jetty.servlet.FilterHolder;
import org.eclipse.jetty.servlet.ServletContextHandler;

import javax.servlet.DispatcherType;

import org.eclipse.jetty.servlets.CrossOriginFilter;
import org.eclipse.jetty.websocket.jsr356.server.deploy.WebSocketServerContainerInitializer;

import Connector.CommunicatorServerWebSocketEndpoint;

import javax.websocket.server.ServerContainer;
import java.util.Date;
import java.util.EnumSet;
import java.util.logging.Level;

public class CommunicatorServer {

    private static final int PORT = Integer.parseInt(PropertiesLoader.getPropValues("gameServer.port", "application.properties"));

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        startWebSocketServer();
    }

    // Start the web socket server
    private static void startWebSocketServer() {
        ChatLogger.Log(Level.INFO, "Starting server at " + new Date());
        Server webSocketServer = new Server();
        ServerConnector connector = new ServerConnector(webSocketServer);
        connector.setPort(PORT);
        webSocketServer.addConnector(connector);

        // Setup the basic application "context" for this application at "/"
        // This is also known as the handler tree (in jetty speak)
        ServletContextHandler webSocketContext = new ServletContextHandler(ServletContextHandler.SESSIONS);
        FilterHolder cors = webSocketContext.addFilter(CrossOriginFilter.class, "/*", EnumSet.of(DispatcherType.REQUEST));
        cors.setInitParameter(CrossOriginFilter.ALLOWED_ORIGINS_PARAM, "*");
        cors.setInitParameter(CrossOriginFilter.ACCESS_CONTROL_ALLOW_ORIGIN_HEADER, "*");
        cors.setInitParameter(CrossOriginFilter.ALLOWED_METHODS_PARAM, "GET,POST,HEAD");
        cors.setInitParameter(CrossOriginFilter.ALLOWED_HEADERS_PARAM, "X-Requested-With,Content-Type,Accept,Origin");
        webSocketContext.setContextPath("/");
        webSocketServer.setHandler(webSocketContext);

        try {
            // Initialize javax.websocket layer
            ServerContainer wscontainer = WebSocketServerContainerInitializer.configureContext(webSocketContext);
            // Add WebSocket endpoint to javax.websocket layer
            wscontainer.addEndpoint(CommunicatorServerWebSocketEndpoint.class);

            webSocketServer.start();
            //server.dump(System.err);

            webSocketServer.join();
        } catch (Throwable t) {
            t.printStackTrace(System.err);
        }
    }
}
