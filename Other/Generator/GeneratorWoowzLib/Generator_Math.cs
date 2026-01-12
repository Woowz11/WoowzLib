public static class Generator_Math{
    public static void Main(){
        foreach(VectorType Type in Enum.GetValues(typeof(VectorType))){
            for(int i = 2; i < 4 + 1; i++){
                CreateVector(Type, i);
            }
        }
    }

    public enum VectorType{ Int, UInt, Float, Double }
    private static void CreateVector(VectorType Type, int N){
        Console.WriteLine("Создание вектора [" + Type + ", " + N + "]");
    }
}