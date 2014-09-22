using System;

namespace lo_novo
{
    // Produces (mostly) short flavour sentences.
    // We'll have to take some liberties with the writing because it's not easy
    //  to compose e.g. 'James, you'll never pull this one off. I'd rather use a dog.'
    // Instead, text will be like (adlib) 'James, you're incredibly ugly.' + 
    //                            (script) 'I think I'm going to trust this task to someone else.'
    // Ease of writing >> good writing
    public static class Adlib
    {
        public static string Compliment(INoun subject = null)
        {
            // e.g. Tom, you're a smart one.
            throw new NotImplementedException();
        }

        public static string Insult(INoun subject = null)
        {
            // e.g. James, you're a dithering idiot.
            // or That orange is worthless.
            // If null, just use 'you'.
            throw new NotImplementedException();
        }

        public static string Threaten(INoun subject = null)
        {
            // e.g. I'm going to kill you.
            // ENEMIES WHO DON'T KNOW YOU SHOULD KEEP SUBJECT SET TO NULL WHEN CALLING THIS.
            // (They shouldn't know your name or much of anything about you.)
            throw new NotImplementedException();
        }

        public static string Tic(INoun noun = null)
        {
            // e.g. SomeGuy sniffles a bit and rubs his nose.
            // If null, probably use 'It'.
            throw new NotImplementedException();
        }
    }
}

