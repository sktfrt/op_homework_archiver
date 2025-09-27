using System.Text;

namespace ArchiverApp;

public static class Archiver
{
    public static string CompressString(string inputLine)
    {
        char? lastC = null;
        int curCnt = 0;
        StringBuilder outputLine = new StringBuilder();

        foreach (char c in inputLine)
        {
            if (lastC == null)
            {
                lastC = c;
                curCnt = 1;
                continue;
            }
            if (c != lastC)
            {
                WriteRun(outputLine, (char)lastC, curCnt);

                curCnt = 1;
                lastC = c;
                continue;
            }
            curCnt += 1;
        }

        WriteRun(outputLine, (char)lastC, curCnt);

        return outputLine.ToString();
    }


    public static string DecompressString(string compressed) // 3a2b
    {
        StringBuilder output = new StringBuilder();

        for (int i = 0; i < compressed.Length; i++)
        {
            if (char.IsDigit(compressed[i]))
            {
                int count = compressed[i] - '0';

                if (i + 1 < compressed.Length && !char.IsDigit(compressed[i + 1]))
                {
                    if (compressed[i + 1] == '\\')
                    {
                        i++;
                    }
                    if (i + 1 < compressed.Length)
                    {
                        output.Append(new string(compressed[i + 1], count));
                        i++;
                        continue;
                    }
                }
            }

            output.Append(compressed[i]);
        }
        return output.ToString();
    }

    public static void WriteRun(StringBuilder output, char symbol, int count)
    {
        if (count == 1)
        {
            output.Append(symbol);
        }
        else if (Char.IsDigit(symbol) && symbol != '\\')
        {
            output.Append(count);
            output.Append('\\');
            output.Append(symbol);
        }
        else
        {
            output.Append(count);
            output.Append(symbol);
        }
    }
}
