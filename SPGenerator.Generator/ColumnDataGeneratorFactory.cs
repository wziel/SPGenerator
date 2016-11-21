using SPGenerator.Generator.ColumnDataGenerator;
using SPGenerator.Generator.ColumnDataGenerator.Boolean;
using SPGenerator.Generator.ColumnDataGenerator.Choice;
using SPGenerator.Generator.ColumnDataGenerator.Currency;
using SPGenerator.Generator.ColumnDataGenerator.DateTime;
using SPGenerator.Generator.ColumnDataGenerator.MultilineText;
using SPGenerator.Generator.ColumnDataGenerator.Number;
using SPGenerator.Generator.ColumnDataGenerator.Text;
using SPGenerator.Model;
using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator
{
    /// <summary>
    /// Factory responsible for creating column data generators.
    /// </summary>
    public class ColumnDataGeneratorFactory : IColumnDataGeneratorFactory
    {
        private Dictionary<Type, IEnumerable<IColumnDataGenerator>> generatorsDic
            = new Dictionary<Type, IEnumerable<IColumnDataGenerator>>();
        
        public ColumnDataGeneratorFactory(
            IEnumerable<IBooleanDataGenerator> booleanGenerators,
            IEnumerable<IChoiceDataGenerator> choiceGenerators,
            IEnumerable<ICurrencyDataGenerator> currencyGenerators,
            IEnumerable<IDateTimeDataGenerator> dateTimeGenerators,
            IEnumerable<IMultilineTextDataGenerator> multilineTextGenerators,
            IEnumerable<INumberDataGenerator> numberGenerators,
            IEnumerable<ITextDataGenerator> textGenerators)
        {
            generatorsDic.Add(typeof(BooleanColumnPOCO), booleanGenerators.ToList());
            generatorsDic.Add(typeof(ChoiceColumnPOCO), choiceGenerators.ToList());
            generatorsDic.Add(typeof(CurrencyColumnPOCO), currencyGenerators.ToList());
            generatorsDic.Add(typeof(DateTimeColumnPOCO), dateTimeGenerators.ToList());
            generatorsDic.Add(typeof(MultilineTextColumnPOCO), multilineTextGenerators.ToList());
            generatorsDic.Add(typeof(NumberColumnPOCO), numberGenerators.ToList());
            generatorsDic.Add(typeof(TextColumnPOCO), textGenerators.ToList());
        }

        public IEnumerable<IColumnDataGenerator> GetDataGenerators(ColumnPOCO columnPOCO)
        {
            var generators = generatorsDic[columnPOCO.GetType()];
            if(generators != null)
            {
                generators = generators.Where(g => g.CanGenerateData(columnPOCO));
                if(generators.Any())
                {
                    return generators;
                }
            }
            throw new GUIVisibleException("Nie udało się znaleźć generatorów danych dla kolumny " + columnPOCO.DisplayName);
        }
    }

    public interface IColumnDataGeneratorFactory
    {
        IEnumerable<IColumnDataGenerator> GetDataGenerators(ColumnPOCO columnPOCO);
    }
}
