using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HddTree
{
    public class ThresholdCollection<T> where T : IThresholdItem
    {
        private readonly int _maxItemCount;
        private readonly long _minThreshold;
        
        private readonly List<T> _items;

        public ThresholdCollection(int maxItemCount, long minThreshold)
        {
            _maxItemCount = maxItemCount;
            _minThreshold = minThreshold;

            _items = new List<T>(maxItemCount);
        }

        public long TotalValue { get; private set; }

        public long LimitedValue => _items.Sum( i => i.Value);

        public void Add(T child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));

            this.TotalValue += child.Value;

            if (child.Value > _minThreshold)
            {
                if (_items.Count < _maxItemCount)
                {
                    _items.Add(child);
                }
                else
                {
                    var minItem = _items.First(i => i.Value == _items.Min(j => j.Value));
                    if (child.Value > minItem.Value)
                    {
                        _items.Remove(minItem);
                        _items.Add(child);
                    }
                }
            }
        }

        public IEnumerable<T> GetTop(int count)
        {
            return this._items.OrderByDescending(i => i.Value).Take(count);
        }

        public IEnumerable<T> GetAllSorted()
        {
            return this._items.OrderByDescending(i => i.Value);
        }
    }
}