// 在 http://fsharp.org 上了解有关 F# 的详细信息
// 请参阅“F# 教程”项目以获取更多帮助。
namespace BLL

module main = 
    [<EntryPoint>]
    let main argv = 
        let mutable arr:int[] = Array.init 5 (fun a ->a)
        printfn "%A" arr
        System.Console.ReadLine() |> ignore
        0 // 返回整数退出代码
