import React from "react"
import Link from "next/link"
function ChoiceMenu(){
    return(
        <div className="w-full bg-gradient-to-r from-blue-600 via-blue-500 to-blue-700 shadow-lg p-4 flex items-center justify-between">

            <div className="flex gap-10 text-lg font-semibold text-white pl-6">
                <Link href="/afterLoginPage" className="hover:text-yellow-300 transition">Strona Główna</Link>
                <Link href="/test" className="hover:text-yellow-300 transition">Panel magazyniera</Link>
                <Link href="/storedProductsPage" className="hover:text-yellow-300 transition">Produkty</Link>
            </div>

            <button className="text-white font-bold px-6 py-2 bg-red-500 rounded-xl hover:bg-red-600 transition shadow-md mr-4">
                Wyloguj
            </button>
        </div>
        
    )
}

export default ChoiceMenu