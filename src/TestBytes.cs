static class TestBytes
{
    private static byte[] bytes { get; } = System.Text.Encoding.UTF8.GetBytes("MOVE 24 X 100\nPING 23\n");
    private static int index { get; set; } = 0;

    public static byte get()
    {
        byte b = bytes[index];
        index = (index + 1) % bytes.Length;
        return b;
    }
}