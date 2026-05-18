
namespace MagicShot_Roulette.Utils
{
    public class Select
    {
        public Select() { }
        public Select(string name) 
        {
            Data = name;
            IsEnable = true;
        }
        public string Data { get; set; }
        public bool IsEnable { get; set; }
        
    }
    public class SelectVisualizer
    {
        private List<Select> datas = new List<Select>();
        int nowSelect = 0;
        public SelectVisualizer() 
        {
            Clear();
        }
        public string GetData(int index)
        {
            if (!IsValidIndex(index)) return null;
            return datas[index].Data;
        }
        public void AddData(string data)
        {
            datas.Add(new Select(data));
        }
        public bool RemoveData(string data) 
        {
            int index = datas.FindIndex(s => s.Data == data);
            if (index == -1) return false;
            datas.RemoveAt(index);
            nowSelect = GetValidIndexForStart();
            return true;
        }
        public void Clear() { datas.Clear();  nowSelect = 0;}

        public void Visualize()
        {
            for (int i = 0; i < datas.Count; i++)
            {
                if(i == nowSelect)
                {
                    Console.ForegroundColor = Colors.BackGroundColor;
                    Console.BackgroundColor = Colors.MainColor; 
                    Console.WriteLine($"{i + 1}: {datas[i].Data}");
                    Console.ForegroundColor = Colors.MainColor;
                    Console.BackgroundColor = Colors.BackGroundColor;
                }
                else if (!datas[i].IsEnable)
                {
                    Console.ForegroundColor = Colors.DisableColor;
                    Console.WriteLine($"{i + 1}: {datas[i].Data}");
                    Console.ForegroundColor = Colors.MainColor;
                }
                else
                { 
                    Console.WriteLine($"{i + 1}: {datas[i].Data}");
                }
            }
        }
        public void NextSelect()
        {
            int availableIndex = GetValidIndex(nowSelect, true);
            nowSelect = Math.Min(datas.Count - 1, availableIndex); 
        }

        public void PrevSelect()
        {
            int availableIndex = GetValidIndex(nowSelect, false);

            nowSelect = Math.Max(0, availableIndex); 
        }

        public int GetSelect()
        {
            return nowSelect;
        }
        public void Disable(int index)
        {
            if (IsValidIndex(index))
            { 
                datas[index].IsEnable = false;
            }
            nowSelect = GetValidIndexForStart();
        }

        public void Enable(int index) 
        { 
            if (IsValidIndex(index))
            {
                datas[index].IsEnable = true;
            }
        }
        private bool IsValidIndex(int index)
        {

            if (index > -1 && index < datas.Count) return true;
            return false;
        }
        private int GetValidIndex(int start, bool isAscend)
        {
            int availableIndex = start;
            if (isAscend)
            {
                for (int i = start + 1; i < datas.Count; i++)
                {
                    if (datas[i].IsEnable) { availableIndex = i; break; }
                }
            }
            else
            { 
                for (int i = start - 1; i > - 1; i--)
                {
                    if (datas[i].IsEnable) { availableIndex = i; break; }
                }
            }
            return availableIndex;
        }
        private int GetValidIndexForStart()
        {
            return GetValidIndex(-1, true);
        }
    }
}
