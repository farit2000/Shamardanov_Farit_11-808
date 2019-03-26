//FirstTask
        public static IEnumerable<int> FirstTask(IEnumerable<int> str)
        {
            return str.Select((x, i) => (i + 1) * x)
            .Where(x => x >= 10 && x <= 99)
                .Reverse();
        }
        
//SecondTask
    public static class IenumerableExtension
    {
        static IEnumerable<Tuple<TItem1, TItem2>> Where<TItem1, TItem2>(this IEnumerable<TItem1> data1, IEnumerable<TItem2> data2 Func<TItem1, TItem2, bool> predicate)
        {
            var list = new List<Tuple<TItem1, TItem2>>();
            foreach (TItem1 value in data1)
            {
                foreach (TItem2 value2 in data2)
                    if (predicate(value, value2)) list.Add(Tuple.Create(value, value2));
            }
            return list;
        }
    }
    
    //ThirdTask
    public static ThirdTask(List<B> B, List<C> C, List<D> D, List<E> E)
        {
            return 
            B.GroupJoin(
                D,
                e => e.CountryMade,
                q => q.Articul,
                (e, q) => new
                {
                    Country = e.CountryMade,
                    Articul = e.Articul,
                    Users = q.Select(p => E.Where(y => y.Articul == p.Articul).Select(z => z.UserId)),
                    Prise = (q.Select(p => D.Where(y => y.Articul == p.Articul).Select(z => z.Prise)).First())
                })
                .Select(e => new
                {
                    Country = e.Country,
                    Articul = e.Articul,
                    countsiDForUsers = C.GroupBy(e.Users, q => new
                    {

                    })

                })
        }
    
