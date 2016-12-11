using SPGenerator.Model;
using SPGenerator.Model.Column;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPGenerator.Generator.ColumnDataGenerator;

namespace SPGenerator.Generator
{
    /// <summary>
    /// Class of objects responsible for generating entries for lists.
    /// </summary>
    public class DataGenerator : IDataGenerator
    {
        private static readonly Random RANDOM = new Random();
        private readonly IColumnDataGeneratorFactory columnDataGeneratorFactory;

        public DataGenerator(IColumnDataGeneratorFactory columnDataGeneratorFactory)
        {
            this.columnDataGeneratorFactory = columnDataGeneratorFactory;
        }

        /// <summary>
        /// Generates entries for list.
        /// </summary>
        /// <param name="list">List for which entries will be generated..</param>
        /// <param name="recordsCount">Number of entries to generate.</param>
        /// <returns>Generated entries.</returns>
        public IEnumerable<EntryPOCO> GenerateData(ListPOCO list, int recordsCount)
        {
            var result = new List<EntryPOCO>(recordsCount);
            Dictionary<ColumnPOCO, List<object>> columnsRawData = GetColumnsRawData(list, recordsCount);
            for(int i = 0; i < recordsCount; ++i)
            {
                var entry = new EntryPOCO();
                foreach(var dataDictionaryEntry in columnsRawData)
                {
                    entry.SetValue(
                        dataDictionaryEntry.Key, 
                        dataDictionaryEntry.Value[i]);
                }
                result.Add(entry);
            }
            return result;
        }

        /// <summary>
        /// Gets data generated for each column.
        /// </summary>
        /// <param name="list">List that contains columns for which data is to be generated.</param>
        /// <param name="recordsCount">Count of entries to generate</param>
        /// <returns>Mapping of data for each column.</returns>
        private Dictionary<ColumnPOCO, List<object>> GetColumnsRawData(ListPOCO list, int recordsCount)
        {
            Dictionary<ColumnPOCO, List<object>> allColumnsRawData = new Dictionary<ColumnPOCO, List<object>>();
            foreach (var column in list.ColumnPOCOList)
            {
                var columnGenerators = columnDataGeneratorFactory.GetDataGenerators(column);
                var entriesCountPerGenerator = GetEntriesCountPerGenerator(columnGenerators, recordsCount);
                var columnRawData = new List<object>();
                foreach (var generator in columnGenerators)
                {
                    var entryCount = entriesCountPerGenerator[generator];
                    var generatorRawData = generator.GenerateData(column, entryCount);
                    columnRawData.AddRange(generatorRawData);
                }
                columnRawData = columnRawData.OrderBy(x => RANDOM.Next()).ToList();
                allColumnsRawData.Add(column, columnRawData);
            }

            return allColumnsRawData;
        }

        /// <summary>
        /// Assigns each generator a number of entries to geenrate per that generaotr.
        /// </summary>
        /// <param name="columnGenerators">All column generators.</param>
        /// <param name="allEntriesCount">Number of all entries to generate.</param>
        /// <returns>Mapping of entries to generate count per generator.</returns>
        private Dictionary<IColumnDataGenerator, int> GetEntriesCountPerGenerator(
            IEnumerable<IColumnDataGenerator> columnGenerators, int allEntriesCount)
        {
            var result = new Dictionary<IColumnDataGenerator, int>();
            var equalDivisionEntryCount = allEntriesCount / columnGenerators.Count();
            var generatorsWithOneMoreEntryCount = allEntriesCount - equalDivisionEntryCount;
            foreach(var generator in columnGenerators)
            {
                int entryCount = equalDivisionEntryCount;
                if(generatorsWithOneMoreEntryCount > 0)
                {
                    entryCount++;
                    generatorsWithOneMoreEntryCount--;
                }
                result.Add(generator, entryCount);
            }
            return result;
        }
    }

    public interface IDataGenerator
    {
        /// <summary>
        /// Generates entries for list.
        /// </summary>
        /// <param name="list">List for which entries will be generated..</param>
        /// <param name="recordsCount">Number of entries to generate.</param>
        /// <returns>Generated entries.</returns>
        IEnumerable<EntryPOCO> GenerateData(ListPOCO list, int recordsCount);
    }
}
