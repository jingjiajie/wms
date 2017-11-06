namespace BLL

type Table()=
    class
        let mutable cells:string[,] = null
        member this.Cells 
            with get() = cells
            and set(value) = cells <- value
    end
