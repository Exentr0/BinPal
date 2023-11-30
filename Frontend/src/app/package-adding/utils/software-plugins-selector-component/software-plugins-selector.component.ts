import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { PluginInterface } from '../../../shared/types/plugin.interface';
import { SoftwareInterface } from '../../../shared/types/software.interface';
import {SoftwareService} from "../../../shared/services/softwareService";

@Component({
    selector: 'app-software-plugins-selector-component',
    templateUrl: './software-plugins-selector.component.html',
    styleUrls: ['./software-plugins-selector.component.css'],
})
export class SoftwarePluginsSelectorComponent implements OnInit {
    @Input() software!: SoftwareInterface;
    pluginOptions: PluginInterface[] = [];
    @Input() preSelectedPlugins: PluginInterface[] = [];
    @Output() selectionChange = new EventEmitter<PluginInterface[]>();

    selectedPlugins: PluginInterface[] = [];

    constructor(private softwareService: SoftwareService) {}

    ngOnInit(): void {
        this.fetchSoftwarePlugins();
        this.selectedPlugins = [...this.preSelectedPlugins];
        console.log('Selected plugins:', this.selectedPlugins);
    }

    onSelectionChange() {
        this.selectionChange.emit(this.selectedPlugins);
    }

    private fetchSoftwarePlugins() {
        this.softwareService.getSoftwarePlugins(this.software.id).subscribe(
            (plugins) => {
                this.pluginOptions = plugins;
            },
            (error) => {
                console.error('Error fetching software plugins:', error);
            }
        );
    }
}
