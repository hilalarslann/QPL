const model = {};

model.fields = [
    {
        name: 'CPC',
        caption: 'CPC'
    },
    {
        name: 'nortelCpc',
        caption: 'Nortel CPC'
    },
    {
        name: 'engineeringCode',
        caption: 'Engineering Code'
    },
    {
        name: 'specNo',
        caption: 'Spec No'
    },
    {
        name: 'description',
        caption: 'Description'
    },
    {
        name: 'comments',
        caption: 'Comments'
    },
    {
        name: 'concept',
        caption: 'Concept'
    },
    {
        name: 'packageType',
        caption: 'Package Type'
    },
    {
        name: 'rohsStatus',
        caption: 'RoHS Status'
    },
    {
        name: 'flammabilityRating',
        caption: 'Flammibility Rating'
    },
    {
        name: 'radiassionRating',
        caption: 'Radiassion Rating'
    },
    {
        name: 'disassembledOrReusable',
        caption: 'Disassembled or Reusable'
    },
    {
        name: 'createdDate',
        caption: 'Created Date'
    },
    {
        name: 'updatedDate',
        caption: 'Updated Date'
    },
    // foreign properties
    {
        name: 'manufacturerCode',
        caption: 'Manufacturer Code'
    }
  ];

model.captions = {};

model.fields.forEach(x => {
    model.captions[x.name] = x.caption;
});

model.summaryFields = [
    "CPC",
    "engineeringCode",
    "concept",
    "specNo",
    "nortelCpc",
    "rohsStatus",
    "packageType",
    "description",
    "comments",
    "radiassionRating"
]

export default model;