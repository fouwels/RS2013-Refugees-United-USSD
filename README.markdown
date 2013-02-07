#R.U.U.S.S.D (RUST)

####Kaelan "kfouwels" Fouwels - James "BillinghamJ" Billingham

Rust is an intermediate service to allow interfacing with the API from any mobile device using USSD.

Rust has been written in C# and is easily scalable & modular. We've built all of its functions with security, cost, usability, speed & flexibility in mind.

Rust makes good use of the concept of "sessions" in the USSD protocol, keeping them as secure as possible & having heavily enforced limits to requests based on authentication status. Authentication never happens outside a USSD session, therefore no details are saved on the device.

To initiate a Rust session, the user either SMSes or calls a standard phone number. A USSD session is then created and a popup will appear on the user's phone providing access to Rust. To review this process in detail, please see the flowchart embedded below.

Rust uses a standard geographic number, which means the service will work globally with all current infrastructure - no additional partnerships would be required with networks. This also reduces costs, both for Refugees United and users.

It has been built against the API documentation from Global USSD. Due to short notice, we do not yet have a demo account with them. As a result, it is not currently possible to authenticate with their API and, as such, Rust cannot be used through its intended interface.

This is something we're looking into to resolve, but in the meantime the code can only be reviewed and run with debug values. However, it will plug straight into Global USSD's systems with very little effort, and switching to another provider should be relatively trivial.

###Command Flow
![Flowchart](flowchartSmall.png)