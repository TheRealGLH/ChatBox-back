package nl.fhict.ChatBox;

import com.google.gson.Gson;

/**
 * Hello world!
 *
 */
public class App 
{
    public static void main( String[] args )
    {
        Gson gson = new Gson();
        
        System.out.println( "Hello World!" + gson.toString());
        //System.out.println( "Hello World!");
    }
}
