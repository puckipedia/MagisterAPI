Hello!

I finally decided to release my Magister API, just because I've waited too long. So, here it is! The system is built out of multiple parts, so I'll mention every one of them:

 - `Poehoe`
    This is the actual code, needed for communicating. This contains the Binair format parser, and requesters for a lot of stuff. The whole system is for now half-documented, and I might fix that. Anyways, send me a pull request and I will look at it.

 - `PoehoeWIP`
    This is a Windows Phone app built with the Poehoe API. Check it out, if you can run the emulator.

 - `PoehoeZipWin32`
    This is a special project, this one contains all the code needed to compress the requests. This one is to be used with Win32 apps, like wpf or console. See *Welp* for an example.

 - `PoehoeZipWinPhone`
    The same as *PoehoeZipWin32*, but just for Windows Phone. Used in *PoehoeWIP*.

 - `Welp`
     An example Console app, mostly used to just experiment with the API.


