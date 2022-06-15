# Description
A simple package to extend SerialPort communication by adding some async helper methods.

# Getting started
After Installation, just go ahead and import the corresponding namespace:

**C#**

    using rskibbe.IO.Ports.Com;
    
**Visual Basic .NET**
    
    Imports rskibbe.IO.Ports.Com

This will enable using the following helper methods & functions.
> Keep in mind to actually **set a correct NewLine property** on the SerialPort-instance itself. Otherwise the functions may not work as you would expect. 

## ReadLineAsync
Read all data bytewise as string, till a NewLine occurence is being found.
> This function **does not include** the **NewLine** String itself.

## ReadLineAsync(CancellationToken)
Same as above, but with the possibility to provide a CancellationToken.

## WriteLineAsync(String)
Writes the target String into the stream, followed by the specified SerialPorts instance NewLine property value as message terminator.
> This function will also Flush the Stream async

## RequestResponseAsync(String)
Many SerialPort devices are built to work with a Request->Response-alike mechanism. Use this function to send some string data and to get the response right afterwards.
> Is the same as using WriteLineAsync first and then getting the response by ReadLineAsync.

## RequestResponseAsync(String, CancellationToken)
Same as above, but with the possibility to provide a CancellationToken.