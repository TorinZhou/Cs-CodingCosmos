using System.Text;

var currentEncoding = Encoding.Unicode;

char letter = 'a';
char digit = '4';
char symbol = '!';
char newLIne = '\n';

char someChar = (char)100;
char omega = (char)937;
Console.WriteLine(omega);

for (char c = 'A'; c < 'z'; c++)
{
    Console.WriteLine(c + ", ");
}


Console.ReadKey();