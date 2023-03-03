class Program
{
    public const byte NEWLINE = 10; //Byte value of newline char
    public const byte SPACE = 32; //Byte value of space char

    public static byte readByte()
    {
        return TestBytes.get(); //Endlessly repeating stream of bytes
    }

    public static void processLine(List<byte[]> tokens) //Console logging to show functionality
    {
        Console.WriteLine("Processing tokens");
        int i = 0;
        foreach (var bytes in tokens)
        {
            Console.Write("Token " + i + ": ");
            foreach (var b in bytes)
            {
                Console.Write(b + " ");
            }
            Console.WriteLine();
            i++;
        }
    }

    public static void run() {
        List<byte> temp = new List<byte>(); //Temp list for unfinished token
        List<byte[]> tokens = new List<byte[]>(); //List of tokens to be sent for processing
        bool isNumber = false; //Flips after every token, so that every other token gets converted to integer

        while (true) //While stream has more bytes
        { 
            Thread.Sleep(200); //For debugging purpose
            
            byte b = readByte(); //Read next byte in stream

            if (b == 0) continue; //Skips empty bytes from 16-bit chars
            if (b == SPACE || b == NEWLINE) //If space or linebreak, create token from temp list of accumulated bytes
            {
                if (!temp.Any()) continue; //Skip if no valid bytes has been added to temp list (double spaces etc.)

                byte[] token = temp.ToArray();
                if (isNumber)
                {
                    char[] c = System.Text.Encoding.UTF8.GetChars(token); //Convert from bytes to chars
                    int i = Int32.Parse(c);
                    token = new byte[]{(byte)i}; //Convert signed int back to unsigned int
                }
                tokens.Add(token);
                temp.Clear();
                isNumber = !isNumber;

                if (b == NEWLINE && tokens.Any())
                {
                    processLine(tokens);
                    tokens.Clear();
                }
            }
            else
            {
                temp.Add(b); 
            }
        }
    }
    
    static void Main(string[] args)
    {
        run();
    }
}