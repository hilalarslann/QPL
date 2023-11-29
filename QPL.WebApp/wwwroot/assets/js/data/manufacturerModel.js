const model = {};

model.fields = [
    {
        name: 'name',
        caption: 'Name'
    },
    {
        name: 'code',
        caption: 'Code'
    },
    {
        name: 'createdDate',
        caption: 'Created Date'
    },
    {
        name: 'updatedDate',
        caption: 'Updated Date'
    }
  ];

model.captions = {};

model.fields.forEach(x => {
    model.captions[x.name] = x.caption;
});

export default model;