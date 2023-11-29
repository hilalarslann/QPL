const model = {};

model.fields = [
    {
        name: 'CPC',
        caption: 'CPC'
    },
    {
        name: 'engineeringCode',
        caption: 'Engineering Code'
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
        name: 'previousEngineeringStatus',
        caption: 'Previous Engineering Status'
    },
    {
        name: 'currentEngineeringStatus',
        caption: 'Current Engineering Status'
    },
    {
        name: 'entryDate',
        caption: 'Entry Date'
    },
    {
        name: 'modifyDate',
        caption: 'Modify Date'
    },
  ];

model.captions = {};

model.fields.forEach(x => {
    model.captions[x.name] = x.caption;
});

model.summaryFields = [
    "CPC",
    "engineeringCode",
    "manufacturerName",
    "manufacturerCode",
    "previousEngineeringStatus",
    "currentEngineeringStatus",
    "entryDate",
    "modifyDate",
]

export default model;