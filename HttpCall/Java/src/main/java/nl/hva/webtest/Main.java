package nl.hva.webtest;

import com.google.gson.Gson;
import com.google.gson.annotations.SerializedName;

import java.io.BufferedInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

/**
 * @author Heiko van der Heijden
 * H.J.M.van.der.heijden@hva.nl
 * A small example reading from a web api
 */
public class Main {

    public static String NOOPS_URL = "https://api.noopschallenge.com/wordbot";

    public static int HTTP_STATUS_CODE_OK = 200;

    public static void main (String args[]) {
        try{
            // Setup the connnection
            URL url = new URL(NOOPS_URL);
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setRequestMethod("GET");
            conn.setRequestProperty("Accept", "application/json");

            // Check if the connection was succesfull
            if (conn.getResponseCode() != HTTP_STATUS_CODE_OK) {
                throw new RuntimeException("Failed : HTTP error code : "
                        + conn.getResponseCode());
            }
            // get the data and parse it to JSON
            InputStream inputStream = new BufferedInputStream(conn.getInputStream());
            Gson gson = new Gson();
            Word result = gson.fromJson(new InputStreamReader(inputStream),Word.class);

            // Print the result
            System.out.println("Result from noops: " + result);

        } catch(IOException ex) {

        }
    }

    /**
     * A wrapper class containing the data from the REST API
     */
    static class Word {
        @SerializedName("words")
        public String[] words;

        @Override
        public String toString() {
            String result = "";
            for (String i : words) {
                result += i;
            }
            return result;
        }
    }
}
