package org.github.nugetmonkey;


/**
 * Created by mehmet on 5/14/2017.
 */

import org.eclipse.jetty.server.Server;

public class TryJetty {

    public static void main(String[] args) throws Exception {
        Server server = new Server(8680);
        try {
            server.start();
            server.dumpStdErr();
            server.join();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
