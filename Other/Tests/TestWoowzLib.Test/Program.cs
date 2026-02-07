using WLO;
using Math = WL.Math;

namespace TestWoowzLib.Test;

public class Program{
    public static void Main(string[] Args){
        try{
            WL.WoowzLib.Start(new WoowzLibInfo(
                Name  : "Test WoowzLib",
                Author: "Woowz11"
            ));
            
            Logger.Info("Начало теста!");
            
            Test_Math();
            
            Logger.Info("Тест завершён!");
        }catch(Exception e){
            throw new Exception("Произошла ошибка при тестах!", e);
        }
    }

    private static void Test_Math(){
        Logger.Info("Тест Math");
        WL.System.Test("Add_Int_0", Math.Add(0, 0), 0);
        WL.System.Test("Add_Int_Pos", Math.Add(1, 1), 2);
        WL.System.Test("Add_Int_Mix", Math.Add(2, -3), -1);
        WL.System.Test("Add_Int_Neg", Math.Add(-10, -5), -15);
        WL.System.Test("Add_Float", Math.Add(0.25f, 0.25f), 0.5f);
        
        WL.System.Test("Sub_Int_0", Math.Sub(0, 0), 0);
        WL.System.Test("Sub_Int_Pos", Math.Sub(2, 3), -1);
        WL.System.Test("Sub_Int_Neg", Math.Sub(-2, -3), 1);
        WL.System.Test("Sub_Int_Mix", Math.Sub(-10, 5), -15);
        WL.System.Test("Sub_Float", Math.Sub(1, 0.25f), 0.75f);
        
        WL.System.Test("Mul_Int_Pos", Math.Mul(2, 3), 6);
        WL.System.Test("Mul_Int_Neg", Math.Mul(6, -6), -36);
        WL.System.Test("Mul_Int_Zero", Math.Mul(1024, 0), 0);
        WL.System.Test("Mul_Int_One", Math.Mul(1024, 1), 1024);
        WL.System.Test("Mul_Float_Pos", Math.Mul(32, 0.1f), 3.2f);
        WL.System.Test("Mul_Float_Float", Math.Mul(0.1f, 0.1f), 0.01f);
        
        WL.System.Test("MulExact_Float", Math.MulExact(0.1f, 0.1f), 0.01f, true);
        WL.System.Test("MulExact_Int", Math.MulExact(2, 3), 6, true);
        
        WL.System.Test("Div_Int", Math.Div(100, 1), 100);
        WL.System.Test("Div_Int_Zero", Math.Div(100, 0), Math.Infinity);
        WL.System.Test("Div_Int_Float", Math.Div(4, 0.1f), 40);
        WL.System.Test("Div_Float", Math.Div(0.25f, 0.5f), 0.5f);
        WL.System.Test("Div_Negative", Math.Div(-10, 2), -5);
        
        WL.System.Test("Div_ByZero_Pos", Math.Div(1, 0f), Math.Infinity);
        WL.System.Test("Div_ByZero_Neg", Math.Div(-1, 0f), -Math.Infinity);
        WL.System.Test("Mul_ByZero", Math.Mul(0f, 123.45f), 0f);
        WL.System.Test("Mul_ByInfinity", Math.Mul(Math.Infinity, 1f), Math.Infinity);
        WL.System.Test("MulExact_Zero", Math.MulExact(0.0f, 123.45f), 0, true);
        
        WL.System.Test("Abs_Pos", Math.Abs(1), 1);
        WL.System.Test("Abs_Zero", Math.Abs(0), 0);
        WL.System.Test("Abs_Neg", Math.Abs(-1), 1);
        WL.System.Test("Abs_Float_Neg", Math.Abs(-0.25f), 0.25f);
        
        WL.System.Test("Sign_Pos", Math.Sign(5), 1);
        WL.System.Test("Sign_Zero", Math.Sign(0), 1);
        WL.System.Test("Sign_Neg", Math.Sign(-5), -1);
        
        WL.System.Test("Pow_2_3", Math.Pow(2, 3), 8);
        WL.System.Test("Sqr_4", Math.Sqr(4), 16);
        WL.System.Test("Cube_3", Math.Cube(3), 27);
        
        WL.System.Test("Root_27_3", Math.Root(27, 3), 3);
        WL.System.Test("Root_NegativeOdd", Math.Root(-8, 3), -2);
        WL.System.Test("Root_NegativeEven", Math.Root(-16, 2), Math.Error);
        WL.System.Test("Sqrt_9", Math.Sqrt(9), 3);
        WL.System.Test("Cbrt_27", Math.Cbrt(27), 3);
        
        WL.System.Test("Round_2.5", Math.Round(2.5f), 3);
        WL.System.Test("Ceil_2.1", Math.Ceil(2.1f), 3);
        WL.System.Test("Floor_2.9", Math.Floor(2.9f), 2);
        WL.System.Test("Truncate_2.9", Math.Truncate(2.9f), 2);
        WL.System.Test("Above_0.1", Math.Above(0.1f), 1);
        WL.System.Test("Above_-0.1", Math.Above(-0.1f), -1);
        
        WL.System.Test("Clamp_5_0_10", Math.Clamp(5, 0, 10), 5);
        WL.System.Test("Clamp_11_0_10", Math.Clamp(11, 0, 10), 10);
        WL.System.Test("Clamp01_0.5", Math.Clamp01(0.5f), 0.5f);
        WL.System.Test("Clamp01_2", Math.Clamp01(2f), 1f);
        WL.System.Test("Lerp_0_10_0.5", Math.Lerp(0, 10, 0.5f), 5f);
        WL.System.Test("LerpD_0_10_0.5", Math.LerpD(0, 10, 0.5f), 5d);
        
        WL.System.Test("Frac_3.75", Math.Frac(3.75f), 0.75f);
        WL.System.Test("Frac_-1.25", Math.Frac(-1.25f), -0.25f);
        WL.System.Test("Mod_10_3", Math.Mod(10, 3), 1f);
        WL.System.Test("Mod_-10_3", Math.Mod(-10, 3), -1f);
        WL.System.Test("Evan_4_2", Math.Evan(4), true);
        WL.System.Test("Evan_5_2", Math.Evan(5), false);
        
        WL.System.Test("ToDeg_PI", Math.ToDeg(Math.PI), 180f);
        WL.System.Test("ToRad_180", Math.ToRad(180f), Math.PI);
        
        WL.System.Test("Sin_0", Math.Sin(0), 0f);
        WL.System.Test("Cos_0", Math.Cos(0), 1f);
        WL.System.Test("Tan_0", Math.Tan(0), 0f);
        
        WL.System.Test("DSin_0", Math.DSin(0), 0.5f);
        WL.System.Test("DCos_0", Math.DCos(0), 1f);
        
        WL.System.Test("IsZero_0", Math.IsZero(0f), true);
        WL.System.Test("IsZero_1e-7", Math.IsZero(1e-7f), true);
        WL.System.Test("IsNear_0.1_0.10000001", Math.IsNear(0.1f, 0.10000001f), true);
        
        WL.System.Test("Fma_2_3_4", Math.Fma(2f, 3f, 4f), 10f);
        
        WL.System.Test("IsInfinity_Pos", Math.IsInfinity(Math.Infinity), true);
        WL.System.Test("IsInfinity_Neg", Math.IsInfinity(Math.NegativeInfinity), true);
        WL.System.Test("IsError_NaN", Math.IsError(Math.Error), true);
    }
}