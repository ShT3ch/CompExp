module ComputationCore


type Segment = 
    {
    Left: double;
    Right: double;
    Length: double;
    }

type Objective = 
    {
    a:double;
    b:double;
    h:double
    }

[<AbstractClass>]
type IntegrateMethod<'NodesSeq>(Func) = 
    abstract member BuildNodes: Segment-> 'NodesSeq
    abstract member DoCalc: 'NodesSeq -> double
    abstract member Order: int
    abstract member Name: string
    member this.F: double -> double = Func


type RectangleIntegrate(Func) = 
    inherit IntegrateMethod<Segment>(Func)
    override this.Name = "Rectangle"
    override this.Order = 2
    override this.BuildNodes inSeg = inSeg
    override this.DoCalc seg = 
            seg.Length*Func ((seg.Left+seg.Right)/2.)

type TrapezoidalIntegrate(Func) = 
    inherit IntegrateMethod<Segment>(Func)
    override this.Name = "Trapezoidal"
    override this.Order = 2
    override this.BuildNodes inSeg = inSeg
    override this.DoCalc seg = 
        (seg.Length/2.)*(Func(seg.Left)+Func(seg.Right))

type SimpsonIntegrate(Func) = 
    inherit IntegrateMethod<Segment>(Func)
    override this.Name = "Simpson"
    override this.Order = 4
    override this.BuildNodes inSeg = inSeg
    override this.DoCalc seg = 
        (seg.Length/6.)*(Func(seg.Left)+4.*Func((seg.Left+seg.Right)/2.)+Func(seg.Right))

let rec split a b h = 
    match b with
    | midEnd when midEnd > a+h -> ({Left = (a); Right = a+h; Length = h})::(split  (a+h) b h)
    | realEnd when realEnd <= a+h -> ({Left = a; Right = b; Length = b-a})::[]

let doCalc<'a> (integrateMethod:IntegrateMethod<'a>, objective) = 
    split objective.a objective.b objective.h
    |> Seq.map(integrateMethod.BuildNodes)
    |> Seq.map(integrateMethod.DoCalc)
    |> Seq.sum

type TableLine(integrateMethod:IntegrateMethod<Segment>, objective) = 
    member this.N = doCalc(integrateMethod, objective)
    member this.DoubledN = doCalc(integrateMethod,{a = objective.a; b = objective.b; h = objective.h/2.})
    member this.DoubledRungeEstimation = (this.DoubledN - this.N)/(2.**((float)integrateMethod.Order))
    member this.Method = integrateMethod

let intagrateMethods func = 
    [   new RectangleIntegrate(func):>IntegrateMethod<Segment>;
        new TrapezoidalIntegrate(func):>IntegrateMethod<Segment>;
        new SimpsonIntegrate(func):>IntegrateMethod<Segment>]

let lines func objective = 
    intagrateMethods  func 
    |> List.map(fun intagrateMethod -> new TableLine(intagrateMethod, objective))

let columnWidth = 11
let doubleOutputAccuracy = 7

let title = (sprintf 
    "| %*s | %*s | %*s | %*s |" 
    columnWidth "Method" 
        columnWidth "Sn" 
            columnWidth "S2n"  
                (columnWidth*2) "Runge R2n")
let delimeter = 
    String.concat "" (Seq.map (fun headChar -> 
        match headChar with
        |'|'->"┼"
        |_ -> "-") title)

let buildStringLineFrom (line:TableLine) = (sprintf 
    "| %*s | %*.*f | %*.*f | %*.*e |" 
        columnWidth line.Method.Name 
            columnWidth doubleOutputAccuracy line.N 
                columnWidth doubleOutputAccuracy line.DoubledN 
                    (columnWidth*2) doubleOutputAccuracy line.DoubledRungeEstimation)

let buildStringLines tableLines = 
    title :: delimeter :: (tableLines |> List.map(fun (x:TableLine) -> buildStringLineFrom(x)))