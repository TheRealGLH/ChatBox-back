import java.sql.*;

import Database.DatabaseConnectorMySql;
import Database.IDatabaseConnector;

class DBRun {
    public static void main(String args[]) {
        IDatabaseConnector databaseConnector = new DatabaseConnectorMySql();
        databaseConnector.getAllChatChannels();
        databaseConnector.getChatChannel("0.15595286540310166");

    }
}