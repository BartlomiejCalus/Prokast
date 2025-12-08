import ChoiceMenu from "@/components/choiceMenu";

export default function taskpanel() {
  const stockData = [
    { id: 1, name: "Palety – A12", qty: "154 szt.", status: "OK" },
    { id: 2, name: "Kartony – B07", qty: "89 szt.", status: "Niski stan" },
    { id: 3, name: "Folie stretch – D02", qty: "42 rolki", status: "OK" },
    { id: 4, name: "Taśmy pakowe – C14", qty: "230 szt.", status: "OK" },
  ];

  const deliveryData = [
    { id: 1, code: "#2025-332", status: "W trakcie rozładunku", carrier: "DHL" },
    { id: 2, code: "#2025-329", status: "Oczekuje na przyjęcie", carrier: "InPost" },
    { id: 3, code: "#2025-321", status: "Przyjęta", carrier: "FedEx" },
  ];

  const deadlineData = [
    { id: 1, title: "Raport miesięczny", date: "30.11.2025" },
    { id: 2, title: "Inwentaryzacja działu A", date: "01.12.2025" },
    { id: 3, title: "Przegląd wózków", date: "15.12.2025" },
  ];

  const priorityTasks = [
    { id: 1, title: "Przygotować przesyłkę dla EXPO", priority: "Wysoki", due: "Dziś" },
    { id: 2, title: "Przenieść towar z sekcji C", priority: "Średni", due: "" },
    { id: 3, title: "Posprzątać stanowisko", priority: "Niski", due: "" },
  ];

  return (
    <div className="w-screen min-h-screen bg-gradient-to-br from-blue-100 via-white to-blue-200 flex flex-col overflow-auto">
      <ChoiceMenu />

      <div className="flex flex-row flex-grow p-6 gap-6">

        <div className="flex flex-col gap-6 w-1/6 bg-white shadow-lg border border-gray-300 rounded-2xl p-6 h-fit">
          <h2 className="font-bold text-2xl text-gray-800">📦 Panel magazynowy</h2>
          <button className="text-lg font-medium text-gray-700 hover:text-blue-600 transition">Stany</button>
          <button className="text-lg font-medium text-gray-700 hover:text-blue-600 transition">Dostawy</button>
          <button className="text-lg font-medium text-gray-700 hover:text-blue-600 transition">Terminy</button>
          <button className="text-lg font-medium text-gray-700 hover:text-blue-600 transition">Zadania Priorytetowe</button>
          <button className="text-lg font-medium text-gray-700 hover:text-blue-600 transition">Niezgodności</button>
        </div>

        <div className="flex flex-col w-5/6 gap-10">
          <h1 className="text-3xl font-bold text-gray-800">🧷 Panel zadań magazynowych</h1>

          <div className="bg-white shadow-lg rounded-2xl overflow-hidden border border-gray-200">
            <h2 className="px-4 py-3 bg-gray-100 text-xl font-semibold text-gray-800">📦 Stany magazynowe</h2>
            <div className="grid grid-cols-4 bg-gray-100 text-gray-700 font-semibold p-4">
              <p>Nazwa</p>
              <p>Ilość</p>
              <p>Status</p>
              <p className="text-center">Akcje</p>
            </div>

            {stockData.map((item, i) => (
              <div
                key={item.id}
                className={`grid grid-cols-4 items-center p-4 ${i % 2 ? "bg-white" : "bg-gray-50"} hover:bg-blue-50 transition`}
              >
                <p>{item.name}</p>
                <p>{item.qty}</p>
                <p>{item.status}</p>
                <div className="flex justify-center gap-4">
                  <button className="border-2 border-blue-600 text-blue-600 px-3 py-1 rounded-lg font-semibold hover:bg-blue-600 hover:text-white transition">Edytuj</button>
                </div>
              </div>
            ))}
          </div>

          {/* TABELA: DOSTAWY */}
          <div className="bg-white shadow-lg rounded-2xl overflow-hidden border border-gray-200">
            <h2 className="px-4 py-3 bg-gray-100 text-xl font-semibold text-gray-800">🚚 Dostawy</h2>
            <div className="grid grid-cols-4 bg-gray-100 text-gray-700 font-semibold p-4">
              <p>Kod</p>
              <p>Status</p>
              <p>Przewoźnik</p>
              <p className="text-center">Akcje</p>
            </div>

            {deliveryData.map((item, i) => (
              <div
                key={item.id}
                className={`grid grid-cols-4 items-center p-4 ${i % 2 ? "bg-white" : "bg-gray-50"} hover:bg-blue-50 transition`}
              >
                <p>{item.code}</p>
                <p>{item.status}</p>
                <p>{item.carrier}</p>
                <div className="flex justify-center gap-4">
                  <button className="border-2 border-blue-600 text-blue-600 px-3 py-1 rounded-lg font-semibold hover:bg-blue-600 hover:text-white transition">Podgląd</button>
                </div>
              </div>
            ))}
          </div>

          {/* TABELA: TERMINY */}
          <div className="bg-white shadow-lg rounded-2xl overflow-hidden border border-gray-200">
            <h2 className="px-4 py-3 bg-gray-100 text-xl font-semibold text-gray-800">⏰ Terminy</h2>
            <div className="grid grid-cols-3 bg-gray-100 text-gray-700 font-semibold p-4">
              <p>Zadanie</p>
              <p>Data</p>
              <p className="text-center">Akcje</p>
            </div>

            {deadlineData.map((item, i) => (
              <div
                key={item.id}
                className={`grid grid-cols-3 items-center p-4 ${i % 2 ? "bg-white" : "bg-gray-50"} hover:bg-blue-50 transition`}
              >
                <p>{item.title}</p>
                <p>{item.date}</p>
                <div className="flex justify-center gap-4">
                  <button className="border-2 border-blue-600 text-blue-600 px-3 py-1 rounded-lg font-semibold hover:bg-blue-600 hover:text-white transition">Edytuj</button>
                </div>
              </div>
            ))}
          </div>

          {/* TABELA: ZADANIA PRIORYTETOWE */}
          <div className="bg-white shadow-lg rounded-2xl overflow-hidden border border-gray-200">
            <h2 className="px-4 py-3 bg-gray-100 text-xl font-semibold text-gray-800">🔥 Zadania priorytetowe</h2>
            <div className="grid grid-cols-3 bg-gray-100 text-gray-700 font-semibold p-4">
              <p>Zadanie</p>
              <p>Priorytet</p>
              <p className="text-center">Akcje</p>
            </div>

            {priorityTasks.map((item, i) => (
              <div
                key={item.id}
                className={`grid grid-cols-3 items-center p-4 ${i % 2 ? "bg-white" : "bg-gray-50"} hover:bg-blue-50 transition`}
              >
                <p>{item.title}</p>
                <p>{item.priority}</p>
                <div className="flex justify-center gap-4">
                  <button className="border-2 border-red-600 text-red-600 px-3 py-1 rounded-lg font-semibold hover:bg-red-600 hover:text-white transition">Usuń</button>
                </div>
              </div>
            ))}
          </div>

          {/* BUTTON: NIEZGODNOŚCI */}
          <button className="self-start px-6 py-3 bg-blue-600 text-white font-semibold rounded-xl shadow hover:bg-blue-700 transition">
            ➕ Zgłoś niezgodność
          </button>
        </div>
      </div>
    </div>
  );
}
