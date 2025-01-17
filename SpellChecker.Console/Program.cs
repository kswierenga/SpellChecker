﻿using SpellChecker.Contracts;
using SpellChecker.Core;
using System.Collections.Generic;

namespace SpellChecker.Console
{

    /// <summary>
    /// The following are the "requirements" for this project:
    /// 
    /// 1. Implement Main() below so that a user can input a sentence.  Each word in that
    ///    sentence will be evaluated with the SpellChecker, which returns true for a word
    ///    that is spelled correctly and false for a word that is spelled incorrectly.  Display
    ///    out each *distinct* word that is misspelled.  That is, if a user uses the same misspelled
    ///    word more than once, simply output that word one time.
    ///    
    ///    Example:
    ///    Please enter a sentance: Salley sells seashellss by the seashore.  The shells Salley sells are surely by the sea.
    ///    Misspelled words: Salley seashellss
    ///    
    /// 2. The concrete implementation of SpellChecker depends on two other implementations of ISpellChecker, DictionaryDotComSpellChecker
    ///    and MnemonicSpellCheckerIBeforeE.  You will need to implement those classes.  See those classes for details.
    ///    
    /// 3. There are covering unit tests in the SpellChecker.Tests library that should be implemented as well.
    /// </summary>
    public static class Program
    {

        /// <summary>
        /// This application is intended to allow a user enter some text (a sentence)
        /// and it will display a distinct list of incorrectly spelled words
        /// </summary>
        /// <param name="args"></param>
        /// 

        public static string stripPunctuation(string word)
        {
            string rslt = "";
            for (int i=0; i < word.Length; i++)
            {
                char aChar = word[i];
                if (!char.IsPunctuation(aChar))
                    rslt += aChar;
            }
            return rslt;
        }
        public static void Main(string[] args)
        {
            System.Console.Write("Please enter a sentance: ");
            var sentence = System.Console.ReadLine();

            // first break the sentance up into words, 
            // then iterate through the list of words using the spell checker
            // capturing distinct words that are misspelled

            // use this spellChecker to evaluate the words
            var spellChecker = new Core.SpellChecker(new ISpellChecker[]
            {
                new MnemonicSpellCheckerIBeforeE(),
                new DictionaryDotComSpellChecker(),
            });

            List<string> misspells = new List<string>();
            string[] words = sentence.Split(' ');
            for (int i=0; i < words.Length; i++)
            {
                string word = words[i];
                word = stripPunctuation(word);
                if (!spellChecker.Check(word))
                {
                    if (misspells.IndexOf(word) < 0)
                        misspells.Add(word);
                }

                
            }

            System.Console.WriteLine("Misspelled words:");
            foreach (string mispelledWord in misspells)
                System.Console.WriteLine(mispelledWord);
            System.Console.ReadLine();
        }

    }

}
