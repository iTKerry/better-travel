namespace global

[<AutoOpen>]
module Async =
    let retn f =
        async {
            return f
        }
