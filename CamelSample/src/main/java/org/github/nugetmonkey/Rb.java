package org.github.nugetmonkey;

import org.apache.camel.builder.RouteBuilder;

/**
 * Created by mehmet on 5/14/2017.
 */

public class Rb extends RouteBuilder {
    public void configure() {
        from("file://test112").to("file://test212");
    }
}