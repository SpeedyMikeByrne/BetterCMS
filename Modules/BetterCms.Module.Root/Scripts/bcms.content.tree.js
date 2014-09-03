﻿/*jslint unparam: true, white: true, browser: true, devel: true */
/*global bettercms */
bettercms.define('bcms.content.tree', ['bcms.jquery', 'bcms', 'bcms.ko.extenders', 'bcms.modal', 'bcms.content', 'bcms.redirect'],
    function ($, bcms, ko, modal, contentModule, redirect) {
    'use strict';

    var tree = {},
        selectors = {
            treeTemplate: '#bcms-contents-tree-template',
            contentTreeContainer: '#bcms-contents-tree',
            sortableContentConnectors: '.bcms-contents-tree-sort-block',
            firstParentRegion: '.bcms-contents-tree-region:first',
            childContents: '.bcms-contents-tree-content'
        },
        links = {},
        globalization = {
            contentsTreeTitle: null,
            closeTreeButtonTitle: null,
            saveSortChanges: null,
            resetSortChanges: null,
            saveSortChangesConfirmation: null
        },
        classes = {
            sortableContentPlaceholder: "bcms-contents-tree-drop-area"
        },
        treeItemTypes = {
            content: 1,
            region: 2
        },
        contentStatuses = {
            draft: 2
        },
        treeViewModel = null;

    // Assign objects to module
    tree.selectors = selectors;
    tree.links = links;
    tree.globalization = globalization;

    function setContentModelValues(contentViewModel, json) {
        if (json.Data.ContentId || json.Data.Id) {
            contentViewModel.contentId = json.Data.ContentId || json.Data.Id;
        }
        if (json.Data.DesirableStatus || json.Data.HasDraft) {
            contentViewModel.draft = json.Data.DesirableStatus == contentStatuses.draft || json.Data.HasDraft;
        }
        if (json.Data.Title || json.Data.WidgetName) {
            contentViewModel.title = json.Data.Title || json.Data.WidgetName;
        }
        if (json.Data.ContentVersion || json.Data.Version) {
            contentViewModel.contentVersion = json.Data.ContentVersion || json.Data.Version;
        }
        contentViewModel.contentType = json.Data.ContentType;
        if (json.Data.PageContentId) {
            contentViewModel.pageContentId = json.Data.PageContentId;
        }
        if (json.Data.PageContentVersion) {
            contentViewModel.pageContentVersion = json.Data.PageContentVersion;
        }
    }

    function createRegionViewModel(regionModel, parentContent, level) {
        var model = new TreeItemViewModel(),
            childContents,
            itemModel,
            i;

        model.id = regionModel.id;
        model.title(regionModel.title);
        model.model = regionModel;
        model.type = treeItemTypes.region;
        model.isInvisible = regionModel.isInvisible;
        model.parentContent = parentContent;
        model.level(level);

        // Collect child contents
        childContents = regionModel.getChildContents();
        level++;
        for (i = 0; i < childContents.length; i++) {
            itemModel = createContentViewModel(childContents[i], model, level);

            model.items.push(itemModel);
        }

        model.addContent = function () {
            regionModel.onAddContent(function (json) {
                treeViewModel.reloadPage = true;

                var contentViewModel = new contentModule.ContentViewModel(null, null, regionModel.parentPageContentId);

                contentViewModel.isInvisible = true;
                setContentModelValues(contentViewModel, json);

                contentViewModel.initializeContent();
                model.items.push(createContentViewModel(contentViewModel, model, model.level() + 1));
            });
        };

        model.removeContent = function (contentViewModel) {
            model.items.remove(contentViewModel);
        };

        return model;
    }

    function createContentViewModel(contentModel, parentRegion, level) {
        var model = new TreeItemViewModel(),
            childRegions,
            itemModel,
            i;

        model.id = contentModel.contentId;
        model.title(contentModel.title);
        model.model = contentModel;
        model.type = treeItemTypes.content;
        model.isInvisible = contentModel.isInvisible;
        model.parentRegion = parentRegion;
        model.level(level);

        childRegions = contentModel.getChildRegions();
        level++;
        for (i = 0; i < childRegions.length; i++) {
            itemModel = createRegionViewModel(childRegions[i], model, level);

            model.items.push(itemModel);
        }

        model.editItem = function () {
            contentModel.onEditContent(function(json) {
                treeViewModel.reloadPage = true;

                setContentModelValues(model.model, json);
                model.title(model.model.title);
            });
        };

        model.deleteItem = function () {
            contentModel.onDeleteContent(function () {
                treeViewModel.reloadPage = true;

                model.parentRegion.removeContent(model);
            });
        };

        model.history = function() {
            contentModel.onContentHistory();
        };

        model.configure = function() {
            contentModel.onConfigureContent(function() {
                treeViewModel.reloadPage = true;
            });
        };

        return model;
    }

    /*
     * Regions/contents tree list view model
     */
    function TreeViewModel(pageModel) {
        var self = this,
            i,
            itemModel;

        self.pageModel = pageModel;
        self.items = ko.observableArray();
        self.visibleItems = ko.observableArray();
        self.invisibleItems = ko.observableArray();
        self.isBeingDragged = ko.observable(false);
        self.reloadPage = false;
        self.contentsSorted = false;

        self.currentLevel = ko.observable(0);

        // Collect child regions
        for (i = 0; i < pageModel.regions.length; i++) {
            if (pageModel.regions[i].parentRegion) {
                continue;
            }
            itemModel = createRegionViewModel(pageModel.regions[i], null, 1);
            if (itemModel.isInvisible) {
                self.invisibleItems.push(itemModel);
            } else {
                self.visibleItems.push(itemModel);
            }
            self.items.push(itemModel);
        }

        self.getItemById = function (itemId, items) {
            var j, 
                item;

            if (!items) {
                items = treeViewModel.items();
            }

            for (j = 0; j < items.length; j ++) {
                if (items[j].id == itemId) {
                    return items[j];
                }

                item = self.getItemById(itemId, items[j].items());
                if (item) {
                    return item;
                }
            }

            return null;
        };

        return self;
    }

    /*
     * Tree item (region or content) view model
     */
    function TreeItemViewModel() {
        var self = this;

        self.items = ko.observableArray();
        self.model = null;
        self.title = ko.observable();
        self.type = null;
        self.id = null;
        self.isInvisible = false;
        self.types = treeItemTypes;

        self.editItem = function () { };
        self.deleteItem = function () { };
        self.addContent = function () { };
        self.history = function () { };
        self.configure = function () { };

        self.isActive = ko.observable(false);
        self.level = ko.observable(0);

        self.isBeingDragged = ko.observable(false);
        self.isBeingDragged.subscribe(function(newValue) {
            treeViewModel.isBeingDragged(newValue);
        });

        self.onMouseLeave = function() {
            this.isActive(false);

            treeViewModel.currentLevel(self.level() - 1);
        };

        self.onMouseEnter = function () {
            this.isActive(true);

            treeViewModel.currentLevel(this.level());
        };

        self.isHover = ko.computed(function() {
            return (!treeViewModel || !treeViewModel.isBeingDragged())
                && self.isActive()
                && self.level() == treeViewModel.currentLevel();
        });

        return self;
    }

    /*
    * Opens modal window with all regions / contents listed
    */
    function onEditContentsTree(data) {

        var pageModel = data.pageViewModel,
            regionModel = data.regionViewModel,
            changedRegions = contentModule.turnSortModeOff(false, true),
            doNotsaveButton,
            dialog,
            i;

        if (changedRegions.length > 0) {

            doNotsaveButton = new modal.button(globalization.resetSortChanges, null, 5, function () {
                contentModule.turnSortModeOff(true);
                contentModule.turnSortModeOn(regionModel);
                dialog.close();

                // Open pages structure modal after user resets changes
                openContentsTree(pageModel);
            });

            dialog = modal.confirm({
                content: globalization.saveSortChangesConfirmation,
                acceptTitle: globalization.saveSortChanges,
                buttons: [doNotsaveButton],
                onAccept: function () {
                    for (i = 0; i < changedRegions.length; i++) {
                        changedRegions[i].contents = changedRegions[i].changedContents;
                    }
                    contentModule.saveContentChanges(changedRegions, function () {
                        // Open pages structure modal after user accepts changes
                        openContentsTree(pageModel, function() {
                            treeViewModel.reloadPage = true;
                        });
                    });
                }
            });
        } else {
            // Open pages structure modal, when there are no changes
            openContentsTree(pageModel);
        }
    }

    function openContentsTree(pageModel, onModelCreated) {
        modal.open({
            title: globalization.contentsTreeTitle,
            cancelTitle: globalization.closeTreeButtonTitle,
            disableAccept: true,
            onLoad: function(dialog) {
                var container = $($(selectors.treeTemplate).html());
                dialog.setContent(container);

                treeViewModel = new TreeViewModel(pageModel);
                if ($.isFunction(onModelCreated)) {
                    onModelCreated();
                }

                ko.applyBindings(treeViewModel, container.get(0));
            },

            onClose: function () {
                if (treeViewModel.contentsSorted) {
                    var changedRegions = checkIfRegionContentsChanged([], treeViewModel.items());
                    if (changedRegions.length > 0) {
                        contentModule.saveContentChanges(changedRegions);

                        return;
                    }
                }

                if (treeViewModel.reloadPage) {
                    redirect.ReloadWithAlert();
                }
            }
        });
    }

    /*
    * Checks if order of contents has changed and if there are changes, saves the changes. 
    */
    function checkIfRegionContentsChanged(changedRegions, treeItems) {
        var l = treeItems.length,
            i,
            j,
            treeItem,
            subTreeItem,
            contentsBeforeReorder,
            contentsAfterReorder,
            hasContentsChanged;

        for (i = 0; i < l; i++) {
            treeItem = treeItems[i];

            if (treeItem.type == treeItemTypes.region) {
                contentsBeforeReorder = treeItem.model.contents;
                contentsAfterReorder = [];

                for (j = 0; j < treeItem.items().length; j++) {
                    subTreeItem = treeItem.items()[j];

                    if (subTreeItem.type == treeItemTypes.content) {
                        contentsAfterReorder.push(subTreeItem.model);
                    }

                    changedRegions = checkIfRegionContentsChanged(changedRegions, subTreeItem.items());
                }

                hasContentsChanged = contentModule.hasContentsOrderChanged(contentsBeforeReorder, contentsAfterReorder);
                if (hasContentsChanged) {
                    treeItem.model.contents = contentsAfterReorder;

                    changedRegions.push(treeItem.model);
                }
            }
        }

        return changedRegions;
    }

    /**
    * Helper function to add knockout binding 'draggable'.
    */
    function addDraggableBinding() {
        ko.bindingHandlers.draggableContent = {
            init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
                if (!valueAccessor()) {
                    return;
                }

                var dragObject = viewModel,
                    isUpdating = false,
                    setup = {
                        connectWith: selectors.sortableContentConnectors,
                        dropOnEmpty: true,
                        placeholder: classes.sortableContentPlaceholder,
                        tolerance: "intersect",
                        start: function () {
                            isUpdating = true;
                            if (dragObject.isBeingDragged) {
                                dragObject.isBeingDragged(true);
                            }
                        },
                        stop: function () {
                            if (dragObject.isBeingDragged) {
                                dragObject.isBeingDragged(false);
                            }
                        },
                        update: function (e, data) {
                            if (isUpdating) {
                                treeViewModel.contentsSorted = true;
                                isUpdating = false;

                                var regionContainer = data.item.parents(selectors.firstParentRegion),
                                    regionId = regionContainer.data('id'),
                                    regionModelBefore = dragObject.parentRegion,
                                    regionModelAfter = regionId == regionModelBefore.id ? regionModelBefore : treeViewModel.getItemById(regionId),
                                    correctOrder = [],
                                    updateOrder = false,
                                    allItems = regionModelAfter.items();

                                if (regionModelBefore != regionModelAfter) {
                                    dragObject.parentRegion = regionModelAfter;

                                    regionModelBefore.removeContent(dragObject);
                                }

                                i = 0;
                                regionContainer.find(selectors.childContents).each(function() {
                                    var id = $(this).data('id'),
                                        itemModel = (id == dragObject.id && regionModelBefore != regionModelAfter)
                                            ? dragObject
                                            : treeViewModel.getItemById(id, allItems);

                                    if (itemModel.type == treeItemTypes.content && itemModel.parentRegion == regionModelAfter) {
                                        correctOrder.push(itemModel);
                                        if (!updateOrder && (!allItems[i] || itemModel.id != allItems[i].id)) {
                                            updateOrder = true;
                                        }
                                    }

                                    i++;
                                });
                                if (updateOrder) {
                                    regionModelAfter.items.removeAll();
                                    for (var i = 0; i < correctOrder.length; i++) {
                                        regionModelAfter.items.push(correctOrder[i]);
                                    }
                                } else {
                                    regionModelAfter.items.push(dragObject);
                                }
                            }
                        }
                    };

                $(element).sortable(setup);
            }
        };
    }

    /**
    * Initializes contents tree module.
    */
    tree.init = function () {
        bcms.logger.debug('Initializing contents tree module');

        addDraggableBinding();
    };

    /**
    * Subscribe to events
    */
    bcms.on(bcms.events.editContentsTree, onEditContentsTree);

    /**
    * Register initialization
    */
    bcms.registerInit(tree.init);

    return tree;
});