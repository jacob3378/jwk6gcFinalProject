using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace jwk6gcFinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Player player1;
        Dealer dealer;
        List<Card> deck = new List<Card>();
        int cardPlayed = 0;
        int playerSpace = 10;// this is where the card should be placed
        int dealerSpace = 10;
        public MainWindow()
        {
            InitializeComponent();
            startScreen.Visibility = Visibility.Visible;
        }

        public String[] addCardPaths()
        {
            String[] cardImgPaths = new string[10];
            for (int i = 0; i < 10; i++)
            {
                if (i == 0)
                {
                    string path = System.IO.Path.Combine(Environment.CurrentDirectory, @"ace.png");
                    cardImgPaths[i] = path;
                }
                else { 
                    string path = System.IO.Path.Combine(Environment.CurrentDirectory, @"card" + (i+1) + ".png");
                    cardImgPaths[i] = path;
                }
            }
            return cardImgPaths;
        }

        

        public void updateWager(int value)
        {
            if (player1.balance - value >= 0)
            {
                player1.wager = player1.wager + value;
                player1.balance = player1.balance - value;
                wagerText.Text = "$" + player1.wager;
                balanceText.Text = "$" + player1.balance;
                reset.Visibility = Visibility.Visible;
                playHandButton.Visibility = Visibility.Visible;
            }
        }


        private void chipButton5(object sender, RoutedEventArgs e)
        {
            updateWager(5);
        }

        private void chipButton10(object sender, RoutedEventArgs e)
        {
            updateWager(10);
        }

        private void chipButton25(object sender, RoutedEventArgs e)
        {
            updateWager(25);
        }

        private void chipButton100(object sender, RoutedEventArgs e)
        {
            updateWager(100);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String[] cardImgPaths = addCardPaths();
            startScreen.Visibility = Visibility.Collapsed;
            player1 = new Player(enteredName.Text, 2000, 0);
            dealer = new Dealer();
            playerName.Text = player1.name;
            balanceText.Text = "$" + player1.balance;
            wagerText.Text = "$" + player1.wager;
            Card card1 = new Card(11, Suit.CLUBS, cardImgPaths[0]);
            Card card2 = new Card(11, Suit.HEARTS, cardImgPaths[0]);
            Card card3 = new Card(11, Suit.DIAMONDS, cardImgPaths[0]);
            Card card4 = new Card(11, Suit.SPADES, cardImgPaths[0]);
            deck.Add(card1);
            deck.Add(card2);
            deck.Add(card3);
            deck.Add(card4);
            for (int i = 1; i < 9; i++)
            {
                Card card12 = new Card(i + 1, Suit.CLUBS, cardImgPaths[i]);
                Card card23 = new Card(i + 1, Suit.HEARTS, cardImgPaths[i]);
                Card card34 = new Card(i + 1, Suit.DIAMONDS, cardImgPaths[i]);
                Card card45 = new Card(i + 1, Suit.SPADES, cardImgPaths[i]);
                deck.Add(card12);
                deck.Add(card23);
                deck.Add(card34);
                deck.Add(card45);
            }
            for (int i = 1; i < 4; i++)
            {
                Card card12 = new Card(10, Suit.CLUBS, cardImgPaths[9]);
                Card card23 = new Card(10, Suit.HEARTS, cardImgPaths[9]);
                Card card34 = new Card(10, Suit.DIAMONDS, cardImgPaths[9]);
                Card card45 = new Card(10, Suit.SPADES, cardImgPaths[9]);
                deck.Add(card12);
                deck.Add(card23);
                deck.Add(card34);
                deck.Add(card45);
            }
            deck = ShuffleList<Card>(deck);
            
        }

        public void showCard(Boolean isPlayer, int space)
        {
            BitmapImage myBitmapImage = new BitmapImage();
            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri(deck[cardPlayed].path);
            myBitmapImage.DecodePixelWidth = 128;
            myBitmapImage.EndInit();

            Image myImage = new Image();
            myImage.Width = 40;
            myImage.Height = 50;
            myImage.Source = myBitmapImage;
            if (isPlayer == true)
            {
                playerHand.Children.Add(myImage);
                Canvas.SetLeft(myImage, space);

            }
            else
            {
                dealerHand.Children.Add(myImage);
                Canvas.SetLeft(myImage, space);
            }   
            cardPlayed++;
        }

        private void playHand(object sender, RoutedEventArgs e)
        {
            chip1.Visibility = Visibility.Collapsed;
            chip2.Visibility = Visibility.Collapsed;
            chip3.Visibility = Visibility.Collapsed;
            chip4.Visibility = Visibility.Collapsed;
            reset.Visibility = Visibility.Collapsed;
            playHandButton.Visibility = Visibility.Collapsed;
            hit.Visibility = Visibility.Visible;
            stand.Visibility = Visibility.Visible;
            Boolean isPlayer = true;
            showCard(isPlayer,playerSpace);
            playerGameLogic(playType.HIT);
            playerSpace += 20;
            dealer.cards = dealer.cards + deck[cardPlayed].value;
            showCard(false, dealerSpace);
            dealerSpace += 20;
            showCard(isPlayer, playerSpace);
            playerGameLogic(playType.HIT);
            playerSpace += 20;
            dealer.cards = dealer.cards + deck[cardPlayed].value;
            showCard(false, dealerSpace);
            dealerSpace += 20;
            /*
            playerSpace += 15;
            showCard(isPlayer, playerSpace);
            playerSpace += 15;
            showCard(isPlayer, playerSpace);
            */
        }

        private void resetButton(object sender, RoutedEventArgs e)
        {
            player1.balance = player1.balance + player1.wager;
            player1.wager = 0;
            balanceText.Text = "$" + player1.balance;
            wagerText.Text = "$" + player1.wager;
            reset.Visibility = Visibility.Collapsed;
            playHandButton.Visibility = Visibility.Collapsed;
        }

        enum playType
        {
           HIT, STAND, DOUBLEDOWN
        }

        private void hitButton(object sender, RoutedEventArgs e)
        {       
            showCard(true, playerSpace);
            playerGameLogic(playType.HIT);
            playerSpace += 20;
        }

        private void playerGameLogic(playType play)
        {
            if (play == playType.HIT)
            {
                player1.cards = player1.cards + deck[cardPlayed - 1].value;
                if(player1.cards > 21)
                {
                    result.Text = "BUST";
                    nextButton.Visibility = Visibility.Visible;
                    nextWager();
                }
            }

        }

        public void nextWager()
        {
            playerSpace = 10;// this is where the card should be placed
            dealerSpace = 10;
            player1.cards = 0;
            dealer.cards = 0;
            hit.Visibility = Visibility.Collapsed;
            stand.Visibility = Visibility.Collapsed;
        }

        private void next(object sender, RoutedEventArgs e)
        {
            wagerText.Text = "$0";
            nextWager();
            result.Text = "";
            player1.wager = 0;
            player1.cards = 0;
            chip1.Visibility = Visibility.Visible;
            chip2.Visibility = Visibility.Visible;
            chip3.Visibility = Visibility.Visible;
            chip4.Visibility = Visibility.Visible;
            reset.Visibility = Visibility.Collapsed;
            nextButton.Visibility = Visibility.Collapsed;
            playerHand.Children.Clear();
            dealerHand.Children.Clear();
            if (cardPlayed >= 35)
            {
                deck = ShuffleList<Card>(deck);
                cardPlayed = 0;
            }
        }

        private void standButton(object sender, RoutedEventArgs e)
        {
            hit.Visibility = Visibility.Collapsed;
            stand.Visibility = Visibility.Collapsed;
            dealerGameLogic();
        }

        public void dealerGameLogic()
        {
            while(true){
                showCard(false, dealerSpace);
                if(checkDealerCards() == false)
                {
                    break;
                }
                dealerSpace += 20;
            }
            if(dealer.cards > 21 || dealer.cards < player1.cards)
            {
                result.Text = "WINNER!";
                player1.balance += player1.wager*2;
                balanceText.Text = "$" + player1.balance;
                nextButton.Visibility = Visibility.Visible;
                return;
            }
            if (dealer.cards == player1.cards) 
            {
                result.Text = "PUSH!";
                player1.balance += player1.wager;
                balanceText.Text = "$" + player1.balance;
                nextButton.Visibility = Visibility.Visible;
                return;
            }
            if(dealer.cards > player1.cards)
            {
                result.Text = "LOSER!";
                nextButton.Visibility = Visibility.Visible;
                return;
            }
        }

        public Boolean checkDealerCards()
        {
            dealer.cards = dealer.cards + deck[cardPlayed - 1].value;
            if(dealer.cards > player1.cards || dealer.cards > 21 || dealer.cards == 17)
            {
                return false;
            }
            return true;
        }


        private List<E> ShuffleList<E>(List<E> inputList)
        {
            List<E> randomList = new List<E>();

            Random random = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = random.Next(0, inputList.Count); 
                randomList.Add(inputList[randomIndex]); 
                inputList.RemoveAt(randomIndex);
            }
            return randomList; 
        }
    }
}
