import org.apache.camel.CamelContext;
import org.apache.camel.impl.DefaultCamelContext;

/**
 * Created by mehmet on 5/14/2017.
 */
public class TryCamel {
    public static void main(String args[]) throws Exception {
        CamelContext context = new DefaultCamelContext();
        context.addRoutes(new Rb() );
        context.start();
        Thread.currentThread().join();
    }
}
