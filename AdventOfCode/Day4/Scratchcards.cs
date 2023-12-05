namespace AdventOfCode.Day4;

public class Scratchcards
{
    private int totalCopiedCards = 0;

    public int TotalScratchcardPoints()
    {
        var scratchCard = GetScratchcardInput();
        var total = 0;

        foreach (var card in scratchCard)
        {
            var currentCardScore = 0;

            var (winningNumbersList, cardNumbersList) = GetWinningAndCardNumbers(card);

            foreach (var number in cardNumbersList)
            {
                if (winningNumbersList.Contains(number))
                {
                    if (currentCardScore == 0)
                    {
                        currentCardScore = 1;
                    }
                    else
                    {
                        currentCardScore *= 2;
                    }
                }
            }
            total += currentCardScore;
        }
        return total;
    }

    public int PartTwo()
    {
        var scratchCard = GetScratchcardInput();

        foreach (var card in scratchCard)
        {
            var originalCardCopy = 0;

            var (winningNumbersList, cardNumbersList) = GetWinningAndCardNumbers(card);

            foreach (var number in cardNumbersList)
            {
                if (winningNumbersList.Contains(number))
                {
                    originalCardCopy++;
                }
            }

            if (originalCardCopy > 0)
            {
                var copiedCards = new List<string>();
                for (var i = 1; i < originalCardCopy+1; i++)
                {
                    if ((scratchCard.IndexOf(card) + i) < scratchCard.Count)
                    {
                        copiedCards.Add(scratchCard[scratchCard.IndexOf(card) + i]);
                    }
                }

                CopyCards(scratchCard, copiedCards);
            }
        }

        return totalCopiedCards + scratchCard.Count();
    }

    private void CopyCards(List<string> scratchCard, List<string> originalCopiedCards)
    {
        foreach (var card in originalCopiedCards)
        {
            var cardCopy = 0;

            var (winningNumbersList, cardNumbersList) = GetWinningAndCardNumbers(card);

            totalCopiedCards++;

            foreach (var number in cardNumbersList)
            {
                if (winningNumbersList.Contains(number))
                {
                    cardCopy++;
                }
            }

            if (cardCopy > 0)
            {
                var copiedCards = new List<string>();
                for (var i = 1; i < cardCopy + 1; i++)
                {
                    if ((scratchCard.IndexOf(card) + i) < scratchCard.Count)
                    {
                        copiedCards.Add(scratchCard[scratchCard.IndexOf(card) + i]);
                    }
                }

                CopyCards(scratchCard, copiedCards);
            }
        }
    }

    private (List<string> winningNumbers, List<string> cardNumbers) GetWinningAndCardNumbers(string card)
    {
        string[] parts = card.Split(':');
        var temp = parts[1];
        string[] parts2 = temp.Split("|");

        var winningNumbers = parts2[0];
        var winningNumbersList = winningNumbers.Split(" ").Where(x => int.TryParse(x, out _)).ToList();

        var cardNumbers = parts2[1];
        var cardNumbersList = cardNumbers.Split(" ").Where(x => int.TryParse(x, out _)).ToList();

        return (winningNumbersList, cardNumbersList);
    }

    private List<string> GetScratchcardInput()
    {
        var fileName = "ScratchcardInput.txt";
        var path = Path.Combine(Environment.CurrentDirectory, @"Day4\", fileName);
        var gameRecordFile = File.ReadAllLines(path);
        var gameRecordList = new List<string>(gameRecordFile);
        return gameRecordList;
    }
}
