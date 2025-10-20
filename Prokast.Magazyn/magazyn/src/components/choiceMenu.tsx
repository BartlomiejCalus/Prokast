import React from "react"
import Link from "next/link"
function ChoiceMenu(){
    return(
        <div className="grid grid-cols-2 bg-menuColor align-middle w-full">
            <div className="flex flex-row p-5 gap-10 text-2xl font-medium pl-10">
             <Link href="/afterLoginPage">Strona Główna</Link>
            <Link href="storedProductsPage">Produkty</Link>   
                
            </div>
            <div className="flex flex-row place-content-end">
                <button className="p-5 bg-menuColor   text-white border-solid border-l-2 border-black">Wyloguj</button>
            </div>
            
        </div>
        
    )
}

export default ChoiceMenu