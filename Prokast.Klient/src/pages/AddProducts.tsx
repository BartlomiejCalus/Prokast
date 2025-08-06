import React, { useState } from 'react';

const AddProducts: React.FC = () => {
  const [form, setForm] = useState({
    name: '',
    sku: '',
    ean: '',
    description: '',
    additionalNames: [
      { title: '', region: 0, value: '' }
    ],
    dictionaryParams: [
      { regionID: 0, name: '', type: '', value: '' }
    ],
    customParams: [
      { name: '', type: '', value: '' }
    ],
    photos: [
      { name: '', value: '' }
    ],
    prices: [
      { name: '', regionID: 0, netto: 0, vat: 0, brutto: 0, priceListID: 0 }
    ],
    priceList: {
      name: ''
    }
  });

  const [availableAdditionalNames, setAvailableAdditionalNames] = useState([
    { title: 'Alias 1', region: 1, value: 'alias-1' },
    { title: 'Alias 2', region: 2, value: 'alias-2' }
  ]);

  const [availableDictionaryParams, setAvailableDictionaryParams] = useState([
    { regionID: 1, name: 'Kolor', type: 'tekst', value: 'czerwony' },
    { regionID: 2, name: 'Rozmiar', type: 'liczba', value: '42' }
  ]);

  const [showAdditionalNameModal, setShowAdditionalNameModal] = useState(false);
  const [showDictionaryParamModal, setShowDictionaryParamModal] = useState(false);

  const [newAdditionalName, setNewAdditionalName] = useState({ title: '', region: 0, value: '' });
  const [newDictionaryParam, setNewDictionaryParam] = useState({ regionID: 0, name: '', type: '', value: '' });


  const [availableCustomParams, setAvailableCustomParams] = useState([
  { name: 'Materiał', type: 'tekst', value: 'bawełna' },
  { name: 'Sezon', type: 'tekst', value: 'lato' }
  ]);

  const [availablePhotos, setAvailablePhotos] = useState([
  { name: 'Front', value: '/images/front.jpg' },
  { name: 'Tył', value: '/images/back.jpg' }
  ]);

  const [newPhoto, setNewPhoto] = useState({ name: '', value: '' });
  const [showPhotoModal, setShowPhotoModal] = useState(false);


  const [newCustomParam, setNewCustomParam] = useState({ name: '', type: '', value: '' });
  const [showCustomParamModal, setShowCustomParamModal] = useState(false);

  const [availablePrices, setAvailablePrices] = useState([
  {
    name: 'Cena podstawowa',
    regionID: 1,
    netto: 100,
    vat: 23,
    brutto: 123,
    priceListID: 1
  },
  {
    name: 'Cena promocyjna',
    regionID: 2,
    netto: 80,
    vat: 23,
    brutto: 98.4,
    priceListID: 2
  }
]);

const [newPrice, setNewPrice] = useState({
  name: '',
  regionID: 0,
  netto: 0,
  vat: 0,
  brutto: 0,
  priceListID: 0
});

const [showPriceModal, setShowPriceModal] = useState(false);

const [availablePriceLists, setAvailablePriceLists] = useState([
  { name: 'Standardowa' },
  { name: 'Promocyjna' }
]);

const [newPriceList, setNewPriceList] = useState({ name: '' });
const [showPriceListModal, setShowPriceListModal] = useState(false);




  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setForm(prev => ({ ...prev, [name]: value }));
  };

  const handleNestedChange = (index: number, field: string, value: any, arrayName: keyof typeof form) => {
    const updatedArray = [...(form[arrayName] as any[])];
    updatedArray[index] = { ...updatedArray[index], [field]: value };
    setForm(prev => ({ ...prev, [arrayName]: updatedArray }));
  };

  const handlePriceListChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm(prev => ({
      ...prev,
      priceList: { name: e.target.value }
    }));
  };

  const handleAdditionalNameSelect = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const selectedValue = e.target.value;
    if (selectedValue === '__add_new__') {
      setShowAdditionalNameModal(true);
    } else {
      const selected = availableAdditionalNames.find(item => item.value === selectedValue);
      if (selected) {
        setForm(prev => ({
          ...prev,
          additionalNames: [selected]
        }));
      }
    }
  };

  const handleDictionaryParamSelect = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const selectedValue = e.target.value;
    if (selectedValue === '__add_new__') {
      setShowDictionaryParamModal(true);
    } else {
      const selected = availableDictionaryParams.find(item => item.value === selectedValue);
      if (selected) {
        setForm(prev => ({
          ...prev,
          dictionaryParams: [selected]
        }));
      }
    }
  };

  const handleCustomParamSelect = (e: React.ChangeEvent<HTMLSelectElement>) => {
  const selectedValue = e.target.value;
  if (selectedValue === '__add_new__') {
    setShowCustomParamModal(true);
  } else {
    const selected = availableCustomParams.find(item => item.value === selectedValue || item.name === selectedValue);
    if (selected) {
      setForm(prev => ({
        ...prev,
        customParams: [selected]
      }));
    }
  }
};

const handlePhotoSelect = (e: React.ChangeEvent<HTMLSelectElement>) => {
  const selectedValue = e.target.value;
  if (selectedValue === '__add_new__') {
    setShowPhotoModal(true);
  } else {
    const selected = availablePhotos.find(p => p.value === selectedValue);
    if (selected) {
      setForm(prev => ({
        ...prev,
        photos: [selected]
      }));
    }
  }
};

const handlePriceSelect = (e: React.ChangeEvent<HTMLSelectElement>) => {
  const selectedValue = e.target.value;
  if (selectedValue === '__add_new__') {
    setShowPriceModal(true);
  } else {
    const selected = availablePrices.find(p => p.name === selectedValue);
    if (selected) {
      setForm(prev => ({
        ...prev,
        prices: [selected]
      }));
    }
  }
};

const handlePriceListSelect = (e: React.ChangeEvent<HTMLSelectElement>) => {
  const selectedValue = e.target.value;
  if (selectedValue === '__add_new__') {
    setShowPriceListModal(true);
  } else {
    const selected = availablePriceLists.find(p => p.name === selectedValue);
    if (selected) {
      setForm(prev => ({
        ...prev,
        priceList: selected
      }));
    }
  }
};


  const handleAddAdditionalName = () => {
    setAvailableAdditionalNames(prev => [...prev, newAdditionalName]);
    setForm(prev => ({ ...prev, additionalNames: [newAdditionalName] }));
    setNewAdditionalName({ title: '', region: 0, value: '' });
    setShowAdditionalNameModal(false);
  };

  const handleAddDictionaryParam = () => {
    setAvailableDictionaryParams(prev => [...prev, newDictionaryParam]);
    setForm(prev => ({ ...prev, dictionaryParams: [newDictionaryParam] }));
    setNewDictionaryParam({ regionID: 0, name: '', type: '', value: '' });
    setShowDictionaryParamModal(false);
  };

  const handleAddCustomParam = () => {
  setAvailableCustomParams(prev => [...prev, newCustomParam]);
  setForm(prev => ({ ...prev, customParams: [newCustomParam] }));
  setNewCustomParam({ name: '', type: '', value: '' });
  setShowCustomParamModal(false);
  };

  const handleAddPhoto = () => {
    setAvailablePhotos(prev => [...prev, newPhoto]);
    setForm(prev => ({ ...prev, photos: [newPhoto] }));
    setNewPhoto({ name: '', value: '' });
    setShowPhotoModal(false);
  };

  const handleAddPrice = () => {
  setAvailablePrices(prev => [...prev, newPrice]);
  setForm(prev => ({ ...prev, prices: [newPrice] }));
  setNewPrice({
    name: '',
    regionID: 0,
    netto: 0,
    vat: 0,
    brutto: 0,
    priceListID: 0
  });
  setShowPriceModal(false);
  };

  const handleAddPriceList = () => {
  setAvailablePriceLists(prev => [...prev, newPriceList]);
  setForm(prev => ({ ...prev, priceList: newPriceList }));
  setNewPriceList({ name: '' });
  setShowPriceListModal(false);
 };


  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    console.log('Wysyłane dane:', form);
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gradient-to-br from-blue-100 via-white to-blue-200">
      <form onSubmit={handleSubmit} className="w-full max-w-2xl p-6 bg-white/80 backdrop-blur-md shadow-lg rounded-2xl space-y-4">
        <h2 className="text-2xl font-bold text-center text-gray-800">Dodaj produkt</h2>

        <input name="name" placeholder="Nazwa" className="w-full p-2 border rounded-xl" value={form.name} onChange={handleChange} />
        <input name="sku" placeholder="SKU" className="w-full p-2 border rounded-xl" value={form.sku} onChange={handleChange} />
        <input name="ean" placeholder="EAN" className="w-full p-2 border rounded-xl" value={form.ean} onChange={handleChange} />
        <textarea name="description" placeholder="Opis" className="w-full p-2 border rounded-xl" value={form.description} onChange={handleChange} />

        <h3 className="font-semibold mt-4">Dodatkowa nazwa</h3>
        <select
          value={form.additionalNames[0]?.value || ''}
          onChange={handleAdditionalNameSelect}
          className="w-full p-2 border rounded-xl"
        >
          <option value="">-- Wybierz dodatkową nazwę --</option>
          {availableAdditionalNames.map((item, i) => (
            <option key={i} value={item.value}>
              {item.title} (Region {item.region})
            </option>
          ))}
          <option value="__add_new__" className="text-blue-500 font-semibold">+ Dodaj nową nazwę</option>
        </select>

        <h3 className="font-semibold mt-4">Parametry słownikowe</h3>
        <select
          value={form.dictionaryParams[0]?.value || ''}
          onChange={handleDictionaryParamSelect}
          className="w-full p-2 border rounded-xl"
        >
          <option value="">-- Wybierz parametr słownikowy --</option>
          {availableDictionaryParams.map((item, i) => (
            <option key={i} value={item.value}>
              {item.name} ({item.type}) – {item.value} [Region {item.regionID}]
            </option>
          ))}
          <option value="__add_new__" className="text-blue-500 font-semibold">+ Dodaj nowy parametr</option>
        </select>

        <h3 className="font-semibold mt-4">Własne parametry</h3>
        <select
          value={form.customParams[0]?.name || ''}
          onChange={handleCustomParamSelect}
          className="w-full p-2 border rounded-xl"
        >
          <option value="">-- Wybierz własny parametr --</option>
          {availableCustomParams.map((item, i) => (
            <option key={i} value={item.name}>
              {item.name} ({item.type}) – {item.value}
            </option>
          ))}
          <option value="__add_new__" className="text-blue-500 font-semibold">+ Dodaj nowy parametr</option>
        </select>


        <h3 className="font-semibold mt-4">Zdjęcie</h3>
        <select
          value={form.photos[0]?.value || ''}
          onChange={handlePhotoSelect}
          className="w-full p-2 border rounded-xl"
        >
          <option value="">-- Wybierz zdjęcie --</option>
          {availablePhotos.map((item, i) => (
            <option key={i} value={item.value}>
              {item.name} ({item.value})
            </option>
          ))}
          <option value="__add_new__" className="text-blue-500 font-semibold">+ Dodaj nowe zdjęcie</option>
        </select>


        <h3 className="font-semibold mt-4">Cena</h3>
        <select
          value={form.prices[0]?.name || ''}
          onChange={handlePriceSelect}
          className="w-full p-2 border rounded-xl"
        >
          <option value="">-- Wybierz cenę --</option>
          {availablePrices.map((item, i) => (
            <option key={i} value={item.name}>
              {item.name} – Netto: {item.netto} zł, VAT: {item.vat}%, Brutto: {item.brutto} zł (Region {item.regionID})
            </option>
          ))}
          <option value="__add_new__" className="text-blue-500 font-semibold">+ Dodaj nową cenę</option>
        </select>


        <h3 className="font-semibold mt-4">Lista cenowa</h3>
        <select
          value={form.priceList.name || ''}
          onChange={handlePriceListSelect}
          className="w-full p-2 border rounded-xl"
        >
          <option value="">-- Wybierz listę cenową --</option>
          {availablePriceLists.map((item, i) => (
            <option key={i} value={item.name}>
              {item.name}
            </option>
          ))}
          <option value="__add_new__" className="text-blue-500 font-semibold">+ Dodaj nową listę</option>
        </select>

        <button
          type="submit"
          className="w-full bg-blue-500 hover:bg-blue-600 text-white p-2 rounded-xl transition"
        >
          Zapisz produkt
        </button>

        {/* MODAL: DODAJ DODATKOWĄ NAZWĘ */}
        {showAdditionalNameModal && (
          <div className="fixed inset-0 bg-black bg-opacity-40 flex items-center justify-center z-50">
            <div className="bg-white p-6 rounded-xl w-full max-w-md space-y-4 shadow-xl">
              <h3 className="text-lg font-semibold">Dodaj nową nazwę</h3>
              <input
                type="text"
                placeholder="Tytuł"
                value={newAdditionalName.title}
                onChange={(e) => setNewAdditionalName(prev => ({ ...prev, title: e.target.value }))}
                className="w-full p-2 border rounded-xl"
              />
              <input
                type="number"
                placeholder="Region"
                value={newAdditionalName.region}
                onChange={(e) => setNewAdditionalName(prev => ({ ...prev, region: Number(e.target.value) }))}
                className="w-full p-2 border rounded-xl"
              />
              <input
                type="text"
                placeholder="Wartość"
                value={newAdditionalName.value}
                onChange={(e) => setNewAdditionalName(prev => ({ ...prev, value: e.target.value }))}
                className="w-full p-2 border rounded-xl"
              />
              <div className="flex justify-end gap-2">
                <button type="button" onClick={() => setShowAdditionalNameModal(false)} className="px-4 py-2 bg-gray-300 rounded-xl">Anuluj</button>
                <button type="button" onClick={handleAddAdditionalName} className="px-4 py-2 bg-blue-500 text-white rounded-xl">Dodaj</button>
              </div>
            </div>
          </div>
        )}

        {/* MODAL: DODAJ PARAMETR SŁOWNIKOWY */}
        {showDictionaryParamModal && (
          <div className="fixed inset-0 bg-black bg-opacity-40 flex items-center justify-center z-50">
            <div className="bg-white p-6 rounded-xl w-full max-w-md space-y-4 shadow-xl">
              <h3 className="text-lg font-semibold">Dodaj nowy parametr słownikowy</h3>
              <input
                type="text"
                placeholder="Nazwa"
                value={newDictionaryParam.name}
                onChange={(e) => setNewDictionaryParam(prev => ({ ...prev, name: e.target.value }))}
                className="w-full p-2 border rounded-xl"
              />
              <input
                type="text"
                placeholder="Typ"
                value={newDictionaryParam.type}
                onChange={(e) => setNewDictionaryParam(prev => ({ ...prev, type: e.target.value }))}
                className="w-full p-2 border rounded-xl"
              />
              <input
                type="text"
                placeholder="Wartość"
                value={newDictionaryParam.value}
                onChange={(e) => setNewDictionaryParam(prev => ({ ...prev, value: e.target.value }))}
                className="w-full p-2 border rounded-xl"
              />
              <input
                type="number"
                placeholder="Region ID"
                value={newDictionaryParam.regionID}
                onChange={(e) => setNewDictionaryParam(prev => ({ ...prev, regionID: Number(e.target.value) }))}
                className="w-full p-2 border rounded-xl"
              />
              <div className="flex justify-end gap-2">
                <button type="button" onClick={() => setShowDictionaryParamModal(false)} className="px-4 py-2 bg-gray-300 rounded-xl">Anuluj</button>
                <button type="button" onClick={handleAddDictionaryParam} className="px-4 py-2 bg-blue-500 text-white rounded-xl">Dodaj</button>
              </div>
            </div>
          </div>
        )}

        {/* MODAL: DODAJ WŁASNY PARAMETR */}
        {showCustomParamModal && (
          <div className="fixed inset-0 bg-black bg-opacity-40 flex items-center justify-center z-50">
            <div className="bg-white p-6 rounded-xl w-full max-w-md space-y-4 shadow-xl">
              <h3 className="text-lg font-semibold">Dodaj własny parametr</h3>
              <input
                type="text"
                placeholder="Nazwa"
                value={newCustomParam.name}
                onChange={(e) => setNewCustomParam(prev => ({ ...prev, name: e.target.value }))}
                className="w-full p-2 border rounded-xl"
              />
              <input
                type="text"
                placeholder="Typ"
                value={newCustomParam.type}
                onChange={(e) => setNewCustomParam(prev => ({ ...prev, type: e.target.value }))}
                className="w-full p-2 border rounded-xl"
              />
              <input
                type="text"
                placeholder="Wartość"
                value={newCustomParam.value}
                onChange={(e) => setNewCustomParam(prev => ({ ...prev, value: e.target.value }))}
                className="w-full p-2 border rounded-xl"
              />
              <div className="flex justify-end gap-2">
                <button type="button" onClick={() => setShowCustomParamModal(false)} className="px-4 py-2 bg-gray-300 rounded-xl">Anuluj</button>
                <button type="button" onClick={handleAddCustomParam} className="px-4 py-2 bg-blue-500 text-white rounded-xl">Dodaj</button>
              </div>
            </div>
          </div>
        )}

        {/* MODAL: DODAJ ZDJĘCIE */}
        {showPhotoModal && (
          <div className="fixed inset-0 bg-black bg-opacity-40 flex items-center justify-center z-50">
            <div className="bg-white p-6 rounded-xl w-full max-w-md space-y-4 shadow-xl">
              <h3 className="text-lg font-semibold">Dodaj zdjęcie</h3>
              <input
                type="text"
                placeholder="Nazwa"
                value={newPhoto.name}
                onChange={(e) => setNewPhoto(prev => ({ ...prev, name: e.target.value }))}
                className="w-full p-2 border rounded-xl"
              />
              <input
                type="text"
                placeholder="Ścieżka URL"
                value={newPhoto.value}
                onChange={(e) => setNewPhoto(prev => ({ ...prev, value: e.target.value }))}
                className="w-full p-2 border rounded-xl"
              />
              <div className="flex justify-end gap-2">
                <button type="button" onClick={() => setShowPhotoModal(false)} className="px-4 py-2 bg-gray-300 rounded-xl">Anuluj</button>
                <button type="button" onClick={handleAddPhoto} className="px-4 py-2 bg-blue-500 text-white rounded-xl">Dodaj</button>
              </div>
            </div>
          </div>
        )}

        {/* MODAL: DODAJ CENĘ */}
        {showPriceModal && (
          <div className="fixed inset-0 bg-black bg-opacity-40 flex items-center justify-center z-50">
            <div className="bg-white p-6 rounded-xl w-full max-w-md space-y-4 shadow-xl">
              <h3 className="text-lg font-semibold">Dodaj nową cenę</h3>
              <input
                type="text"
                placeholder="Nazwa"
                value={newPrice.name}
                onChange={(e) => setNewPrice(prev => ({ ...prev, name: e.target.value }))}
                className="w-full p-2 border rounded-xl"
              />
              <input
                type="number"
                placeholder="Region ID"
                value={newPrice.regionID}
                onChange={(e) => setNewPrice(prev => ({ ...prev, regionID: Number(e.target.value) }))}
                className="w-full p-2 border rounded-xl"
              />
              <input
                type="number"
                placeholder="Netto"
                value={newPrice.netto}
                onChange={(e) => setNewPrice(prev => ({ ...prev, netto: Number(e.target.value) }))}
                className="w-full p-2 border rounded-xl"
              />
              <input
                type="number"
                placeholder="VAT (%)"
                value={newPrice.vat}
                onChange={(e) => setNewPrice(prev => ({ ...prev, vat: Number(e.target.value) }))}
                className="w-full p-2 border rounded-xl"
              />
              <input
                type="number"
                placeholder="Brutto"
                value={newPrice.brutto}
                onChange={(e) => setNewPrice(prev => ({ ...prev, brutto: Number(e.target.value) }))}
                className="w-full p-2 border rounded-xl"
              />
              <input
                type="number"
                placeholder="PriceList ID"
                value={newPrice.priceListID}
                onChange={(e) => setNewPrice(prev => ({ ...prev, priceListID: Number(e.target.value) }))}
                className="w-full p-2 border rounded-xl"
              />
              <div className="flex justify-end gap-2">
                <button type="button" onClick={() => setShowPriceModal(false)} className="px-4 py-2 bg-gray-300 rounded-xl">Anuluj</button>
                <button type="button" onClick={handleAddPrice} className="px-4 py-2 bg-blue-500 text-white rounded-xl">Dodaj</button>
              </div>
            </div>
          </div>
        )}

        {/* MODAL: DODAJ LISTĘ CENOWĄ */}
        {showPriceListModal && (
          <div className="fixed inset-0 bg-black bg-opacity-40 flex items-center justify-center z-50">
            <div className="bg-white p-6 rounded-xl w-full max-w-md space-y-4 shadow-xl">
              <h3 className="text-lg font-semibold">Dodaj listę cenową</h3>
              <input
                type="text"
                placeholder="Nazwa listy cenowej"
                value={newPriceList.name}
                onChange={(e) => setNewPriceList({ name: e.target.value })}
                className="w-full p-2 border rounded-xl"
              />
              <div className="flex justify-end gap-2">
                <button type="button" onClick={() => setShowPriceListModal(false)} className="px-4 py-2 bg-gray-300 rounded-xl">Anuluj</button>
                <button type="button" onClick={handleAddPriceList} className="px-4 py-2 bg-blue-500 text-white rounded-xl">Dodaj</button>
              </div>
            </div>
          </div>
        )}




      </form>
    </div>
  );
};

export default AddProducts;
