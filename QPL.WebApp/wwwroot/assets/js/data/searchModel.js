const model = {};

model.fields = [
    {
        name: 'CPC',
        caption: 'CPC'
    },
    {
        name: 'description',
        caption: 'Description'
    },
    {
        name: 'manufacturerName',
        caption: 'Manufacturer Name'
    },
    {
        name: 'manufacturerCode',
        caption: 'Manufacturer Code'
    },
    {
        name: 'prevEngineeringStatus',
        caption: 'Prev Engineering Status'
    },
    {
        name: 'engineeringStatus',
        caption: 'Engineering Status'
    }
  ];

model.captions = {};

model.fields.forEach(x => {
    model.captions[x.name] = x.caption;
});

model.summaryFields = [
    "CPC",
    "description",
    "manufacturerName",
    "manufacturerCode",
    "prevEngineeringStatus",
    "engineeringStatus"   
]

export default model;