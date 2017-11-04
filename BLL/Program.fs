// 在 http://fsharp.org 上了解有关 F# 的详细信息
// 请参阅“F# 教程”项目以获取更多帮助。

open System

[<EntryPoint>]
let main argv = 
    printfn "%A" "Hello World"
    Console.ReadLine() |> ignore
    0 // 返回整数退出代码
