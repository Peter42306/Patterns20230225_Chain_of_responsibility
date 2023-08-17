using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//=========================================================================
namespace Patterns20230225_Chain_of_responsibility
{
    // ДЗ от 2023 02 11
    // Створити систему спілкування з перекладачами. Коли користувач
    // надсилає повідомлення іншою мовою, система повинна перевести його
    // на мову, яку розуміє перекладач, та надіслати його перекладачеві.
    // Якщо перекладач не може перекласти повідомлення на мову, яку
    // розуміє одержувач, він повинен передати його наступному
    // перекладачеві в ланцюжку.
    // В даному випадку, патерн ланцюжок обов'язків може бути використаний
    // для створення ланцюжка перекладачів. Кожен перекладач представлений
    // окремим об'єктом, який містить інформацію про мову, якою він може
    // перекладати, та посилання на наступний перекладач у ланцюжку.

    internal class Program
    {
        // Класс для переводчиков
        public abstract class Translator
        {
            protected string targetLanguage;
            protected Translator nextTranslator;

            public void SetNextTranslator(Translator translator)
            {
                nextTranslator = translator;
            }

            public virtual void Translate(string message, string targetLanguage)
            {
                if (this.targetLanguage == targetLanguage)
                {
                    Console.WriteLine($"Message translated to {targetLanguage} (imagine that this is translation in) {targetLanguage} : {message}");
                }
                else if (nextTranslator != null)
                {
                    nextTranslator.Translate(message, targetLanguage);
                }
                else
                {
                    Console.WriteLine($"No translator available for {targetLanguage}");
                }
            }
        }

        // Классы для переводчиклв конкретных языков

        public class EnglishTranslator : Translator
        {
            public EnglishTranslator()
            {
                targetLanguage = "English";
            }
        }

        public class FrenchTranslator : Translator
        {
            public FrenchTranslator()
            {
                targetLanguage = "French";
            }
        }

        public class RussianTranslator : Translator
        {
            public RussianTranslator()
            {
                targetLanguage = "Russian";
            }
        }

        public class GermanTranslator : Translator
        {
            public GermanTranslator()
            {
                targetLanguage = "German";
            }
        }

        public class SpanishTranslator : Translator
        {
            public SpanishTranslator()
            {
                targetLanguage = "Spanish";
            }
        }

        static void Main(string[] args)
        {
            var englishTranslator = new EnglishTranslator();
            var frenchTranslator = new FrenchTranslator();
            var russianTranslator = new RussianTranslator();
            var germanTranslator = new GermanTranslator();
            var spanishTranslator = new SpanishTranslator();

            // Устанавливаем связи между переводчиками
            englishTranslator.SetNextTranslator(frenchTranslator);
            frenchTranslator.SetNextTranslator(russianTranslator);
            russianTranslator.SetNextTranslator(germanTranslator);
            germanTranslator.SetNextTranslator(spanishTranslator);

            string message = "Hi, my name is John";

            // Попытка перевести сообщение на разные языки
            englishTranslator.Translate(message, "Romanian");
            englishTranslator.Translate(message, "Polish");
            englishTranslator.Translate(message, "Irish");
            englishTranslator.Translate(message, "Spanish");
            englishTranslator.Translate(message, "Russian");
            englishTranslator.Translate(message, "German");
            englishTranslator.Translate(message, "Italian");
        }
    }
}
//No translator available for Romanian
//No translator available for Polish
//No translator available for Irish
//Message translated to Spanish (imagine that this is translation in) Spanish: Hi, my name is John
//Message translated to Russian (imagine that this is translation in) Russian: Hi, my name is John
//Message translated to German (imagine that this is translation in) German: Hi, my name is John
//No translator available for Italian
//Press any key to continue . . .