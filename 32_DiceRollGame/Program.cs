using DiceRollGame.Game;
using UserCommunication;

var random = new Random();
var dice = new Dice(random);
var consoleUserCommunication = new ConsoleUserCommunication();
var guessingGame = new GuessingGame(dice, consoleUserCommunication);

GameResult gameResult = guessingGame.Play();
guessingGame.PrintResult(gameResult);

Console.ReadKey();






